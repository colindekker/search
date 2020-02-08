namespace D3.Core.Search.Query.Models
{
    using System.Linq;

    public interface IQueryResult<TModel>
        where TModel : class
    {
        int Total { get; set; }

        IQueryable<TModel> Payload { get; set; }
    }

    public interface IQueryResult<TModel, TColumn, TOrder, TGroup, TPredicate, TPredicateValue>
        : IQueryResult<TModel>
        where TModel : class
        where TColumn : IQueryColumn
        where TOrder : IQueryOrder
        where TGroup : IQueryGroup
        where TPredicate : IQueryPredicate<TPredicateValue>
        where TPredicateValue : IQueryPredicateValue
    {
        IQueryConfig<TColumn, TOrder, TGroup, TPredicate, TPredicateValue> QueryConfig { get; set; }
    }
}
