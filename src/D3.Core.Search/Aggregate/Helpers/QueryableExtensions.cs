namespace D3.Core.Search.Aggregate.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using D3.Core.Search.Aggregate.Models;

    public static class QueryableExtensions
    {
        public static Task<AggregateConfig> GetAggregatesAsync<TEntity>([NotNull] this IQueryable<TEntity> result, [NotNull] IAggregateConfig<AggregateColumn> config)
        {
            return AggregateHelper
                .GetAggregatesAsync(result, config);
        }
    }
}
