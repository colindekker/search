namespace D3.Core.Search.Query.Models
{
    using System.Collections.Generic;

    public interface IQueryConfig<TColumn, TOrder, TGroup, TPredicate, TPredicateValue>
        : IQueryConfigBase<TColumn, TOrder, TGroup, TPredicate, TPredicateValue>
        where TColumn : IQueryColumn
        where TOrder : IQueryOrder
        where TGroup : IQueryGroup
        where TPredicate : IQueryPredicate<TPredicateValue>
        where TPredicateValue : IQueryPredicateValue
    {
        List<string> Includes { get; set; }
    }

    public interface IQueryConfig
        : IQueryConfig<QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue>
    {
    }
}
