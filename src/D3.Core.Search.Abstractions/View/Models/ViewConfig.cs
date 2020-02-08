namespace D3.Core.Search.View.Models
{
    using System.Collections.Generic;
    using D3.Core.Search.Aggregate.Models;
    using D3.Core.Search.Query.Models;

    public class ViewConfig
        : IViewConfig<QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue, AggregateColumn, ViewColumn>
    {
        public IAggregateConfig<AggregateColumn> AggregateConfig { get; set; } = new AggregateConfig();

        public IQueryConfig<QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue> QueryConfig { get; set; } = new QueryConfig();

        public IList<ViewColumn> Columns { get; set; } = new List<ViewColumn>();
    }
}
