namespace D3.Core.Search.Query.Handlers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using D3.Core.Search.Query.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IMappingDataQueryConfigHandler<in TQueryConfig>
        where TQueryConfig: IQueryConfig
    {
        Task<TResult> MapAsync<TEntity, TModel, TResult>(
            [NotNull] TQueryConfig query,
            [NotNull] DbContext context,
            CancellationToken cancellationToken = default)
            where TEntity : class
            where TModel : class
            where TResult : class, IQueryResult<TModel>;
    }
}
