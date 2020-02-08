namespace D3.Core.Search.Mapping.EFCore.Query.Handlers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class MappingEFCoreQueryConfigHandler
        : IMappingDataQueryConfigHandler<QueryConfig>
    {
        private readonly IMapper _mapper;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger<MappingEFCoreQueryConfigHandler> _logger;

        public MappingEFCoreQueryConfigHandler(
            [NotNull] IMapper mapper,
            [NotNull] IServiceScopeFactory serviceScopeFactory,
            [NotNull] ILogger<MappingEFCoreQueryConfigHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the asynchronous dynamic querying and mapping of a <see cref="DbSet{TEntity}"/>
        /// that contains entities of type <typeparamref name="TEntity"/> in in <paramref name="context"/>
        /// according to the query defined by <paramref name="query"/>.
        /// The result returns the matching entries mapped to <typeparamref name="TModel"/>
        /// which should have a registered AutoMapper profile how to map to it from <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being queried in <paramref name="context"/>.</typeparam>
        /// <typeparam name="TModel">The type each query result item should be mapped to,</typeparam>
        /// <typeparam name="TResult">The type of the result used to wrap the query result.</typeparam>
        /// <param name="query">The query definition to use to .</param>
        /// <param name="context">The <seealso cref="DbContext"/> containing the <typeparamref name="TEntity"/> entity type <seealso cref="DbSet{TEntity}"/>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the <paramref name="query"/>
        /// when applied to the <seealso cref="DbSet{TEntity}"/> in <paramref name="context"/> of entity type <typeparamref name="TEntity"/>.
        /// </returns>
        public async Task<TResult> MapAsync<TEntity, TModel, TResult>(
            [NotNull] QueryConfig query,
            [NotNull] DbContext context,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TModel : class
            where TResult : class, IQueryResult<TModel>
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider
                .GetService<IDataQueryConfigHandler<QueryConfig>>();

            var result = await handler
                .HandleAsync<TEntity, QueryResult<TEntity>>(query, context, cancellationToken)
                .ConfigureAwait(false);

            IQueryResult<TModel, QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue> payload = new QueryResult<TModel>
            {
                QueryConfig = query,
                Total = 0,
                Payload = null
            };

            try
            {
                var payloadModels = query is { } includeConfig && includeConfig.Includes?.Count > 0
                    ? _mapper
                        .ProjectTo<TModel>(result.Payload, default, includeConfig.Includes.ToArray())
                    : _mapper
                        .ProjectTo<TModel>(result.Payload);

                var payloadCount = await payloadModels
                    .CountAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (payloadCount > 0)
                {
                    payload.Payload = (IQueryable<TModel>)await payloadModels
                        .ToListAsync(cancellationToken)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
            }

            return (TResult)payload;
        }
    }
}
