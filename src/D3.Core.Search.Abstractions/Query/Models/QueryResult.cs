namespace D3.Core.Search.Query.Models
{
    using System.Linq;

    public class QueryResult<TModel>
        : IQueryResult<TModel, QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue>
        where TModel : class
    {
        public IQueryConfig<QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue> QueryConfig { get; set; }

        public int Total { get; set; }

        public IQueryable<TModel> Payload { get; set; }
    }
}
