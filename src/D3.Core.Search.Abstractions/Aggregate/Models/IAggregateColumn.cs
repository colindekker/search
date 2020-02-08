namespace D3.Core.Search.Aggregate.Models
{
    using D3.Core.Search.Aggregate.Values;
    using D3.Core.Search.Column.Models;

    public interface IAggregateColumn
        : IColumn
    {
        bool Visible { get; set; }

        AggregateType Type { get; set; }

        string Left { get; set; }

        string Right { get; set; }
    }
}
