namespace D3.Core.Search.Query.Models
{
    using System.Collections.Generic;

    public interface IQueryConfigBase<TColumn, TOrder, TGroup, TPredicate, TPredicateValue>
        where TColumn : IQueryColumn
        where TOrder : IQueryOrder
        where TGroup : IQueryGroup
        where TPredicate : IQueryPredicate<TPredicateValue>
        where TPredicateValue : IQueryPredicateValue
    {
        List<TColumn> Columns { get; set; }

        List<TOrder> OrderBy { get; set; }

        List<TGroup> GroupBy { get; set; }

        List<TPredicate> QueryBy { get; set; }

        int Page { get; set; }

        int Limit { get; set; }

        int Skip(int total);

        int Take(int total);
    }
}
