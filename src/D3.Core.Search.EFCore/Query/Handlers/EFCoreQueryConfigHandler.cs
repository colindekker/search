namespace D3.Core.Search.EFCore.Query.Handlers
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class EFCoreQueryConfigHandler
        : IDataQueryConfigHandler<QueryConfig>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger<EFCoreQueryConfigHandler> _logger;

        public EFCoreQueryConfigHandler(
            [NotNull] IServiceScopeFactory serviceScopeFactory,
            [NotNull] ILogger<EFCoreQueryConfigHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResult> HandleAsync<TEntity, TResult>(
            [NotNull] QueryConfig query,
            [NotNull] DbContext context,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TResult : class, IQueryResult<TEntity>
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            IQueryable<TEntity> set = context
                .Set<TEntity>()
                .AsNoTracking();

            IQueryResult<TEntity, QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue> payload = null;

            try
            {
                var scope = _serviceScopeFactory.CreateScope();
                var handler = scope.ServiceProvider
                    .GetService<IQueryConfigHandler<QueryConfig>>();

                var result = await handler
                    .HandleAsync<TEntity, QueryResult<TEntity>>(query, set, cancellationToken)
                    .ConfigureAwait(false);

                payload = new QueryResult<TEntity>
                {
                    QueryConfig = query,
                    Total = result.Total,
                    Payload = result.Payload
                };
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }

            return (TResult)payload;
        }
    }
}
