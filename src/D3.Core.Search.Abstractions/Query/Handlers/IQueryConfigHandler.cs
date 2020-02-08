namespace D3.Core.Search.Query.Handlers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using D3.Core.Search.Query.Models;

    public interface IQueryConfigHandler<in TQueryConfig>
        where TQueryConfig: IQueryConfig
    {
        Task<TResult> HandleAsync<TEntity, TResult>(
            [NotNull] TQueryConfig query,
            [NotNull] IQueryable<TEntity> source,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TResult : class, IQueryResult<TEntity>;
    }
}
