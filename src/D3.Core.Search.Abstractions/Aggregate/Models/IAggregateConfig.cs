namespace D3.Core.Search.Aggregate.Models
{
    using System.Collections.Generic;

    public interface IAggregateConfig<TColumn>
        where TColumn : IAggregateColumn
    {
        List<TColumn> Columns { get; set; }
    }
}
