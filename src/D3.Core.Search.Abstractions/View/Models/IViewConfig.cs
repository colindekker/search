namespace D3.Core.Search.View.Models
{
    using System.Collections.Generic;
    using D3.Core.Search.Aggregate.Models;
    using D3.Core.Search.Query.Models;

    public interface IViewConfig<TColumn, TOrder, TGroup, TPredicate, TPredicateValue, TAggregateColumn, TViewColumn>
        where TColumn : IQueryColumn
        where TOrder : IQueryOrder
        where TGroup : IQueryGroup
        where TPredicate : IQueryPredicate<TPredicateValue>
        where TPredicateValue : IQueryPredicateValue
        where TAggregateColumn : IAggregateColumn
        where TViewColumn : IViewColumn
    {
        IAggregateConfig<TAggregateColumn> AggregateConfig { get; set; }

        IQueryConfig<TColumn, TOrder, TGroup, TPredicate, TPredicateValue> QueryConfig { get; set; }

        IList<TViewColumn> Columns { get; set; }
    }
}
