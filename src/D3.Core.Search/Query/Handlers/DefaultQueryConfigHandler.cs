namespace D3.Core.Search.Query.Handlers
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using D3.Core.Extensions;
    using D3.Core.Search.Query.Helpers;
    using D3.Core.Search.Query.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Microsoft.Extensions.Logging;

    public class DefaultQueryConfigHandler
        : IQueryConfigHandler<QueryConfig>
    {
        private readonly ILogger<DefaultQueryConfigHandler> _logger;

        public DefaultQueryConfigHandler(ILogger<DefaultQueryConfigHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResult> HandleAsync<TEntity, TResult>(
            [NotNull] QueryConfig query,
            [NotNull] IQueryable<TEntity> source,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TResult : class, IQueryResult<TEntity>
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IQueryable<TEntity> result = source;

            var total = result.GetType().Implements<IAsyncQueryProvider>()
                ? await result.CountAsync(cancellationToken)
                : result.Count();

            IQueryResult <TEntity, QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue> payload = new QueryResult<TEntity>
            {
                QueryConfig = query,
                Total = total,
                Payload = result
            };

            if (query is { } configWhere)
            {
                result = result.ApplyWhere(configWhere);
            }

            if (result == null)
            {
                return null;
            }

            if (query is { } configOrder)
            {
                result = result.ApplyOrderBy(configOrder);
            }

            try
            {
                payload.Total = result.GetType().Implements<IAsyncQueryProvider>()
                    ? await result.CountAsync(cancellationToken)
                    : result.Count();

                payload.Payload = result
                    .Skip(query.Skip(payload.Total))
                    .Take(query.Take(payload.Total));
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }

            return (TResult)payload;
        }
    }
}
