namespace D3.Core.Search.Mapping.Query.Handlers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using D3.Core.Extensions;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class MappingQueryConfigHandler
        : IMappingQueryConfigHandler<QueryConfig>
    {
        private readonly IMapper _mapper;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger<MappingQueryConfigHandler> _logger;

        public MappingQueryConfigHandler(
            [NotNull] IMapper mapper,
            [NotNull] IServiceScopeFactory serviceScopeFactory,
            [NotNull] ILogger<MappingQueryConfigHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the asynchronous dynamic querying and mapping of a <see cref="IQueryable{T}"/> source that contains entities of type <typeparamref name="TEntity"/> in in <paramref name="source"/>
        /// according to the query defined by <paramref name="query"/>.
        /// The result returns the matching entries mapped to <typeparamref name="TModel"/> which should have a registered AutoMapper profile how to map to it from <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type being queried in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TModel">The type each query result item should be mapped to,</typeparam>
        /// <typeparam name="TResult">The type of the result used to wrap the query result.</typeparam>
        /// <param name="query">The query definition to use to .</param>
        /// <param name="source">The <seealso cref="IQueryable{T}"/> containing the <typeparamref name="TEntity"/> entity type <seealso cref="DbSet{TEntity}"/>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the <paramref name="query"/>
        /// when applied to the <paramref name="source"/> of entity type <typeparamref name="TEntity"/>.
        /// </returns>
        public async Task<TResult> MapAsync<TEntity, TModel, TResult>(
            [NotNull] QueryConfig query,
            [NotNull] IQueryable<TEntity> source,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TModel : class
            where TResult : class, IQueryResult<TModel>
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var result = await handler
                .HandleAsync<TEntity, QueryResult<TEntity>>(query, source, cancellationToken)
                .ConfigureAwait(false);

            IQueryResult<TModel, QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue> payload = new QueryResult<TModel>
            {
                QueryConfig = query,
                Total = result.Total,
                Payload = null
            };

            var payloadModels = query is { } includeConfig && includeConfig.Includes?.Count > 0
                ? _mapper.ProjectTo<TModel>(result.Payload, default, includeConfig.Includes.ToArray())
                : _mapper.ProjectTo<TModel>(result.Payload);

            var total = result.GetType().Implements<IAsyncQueryProvider>()
                ? await result.Payload.CountAsync(cancellationToken).ConfigureAwait(false)
                : result.Payload.Count();

            payload.Total = total;

            if (total > 0)
            {
                try
                {
                    payload.Payload = payloadModels;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return (TResult)payload;
        }
    }
}
