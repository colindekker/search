namespace D3.Core.Search.Aggregate.Models
{
    using D3.Core.Search.Aggregate.Values;
    using D3.Core.Search.Column.Models;

    public class AggregateColumn
        : ColumnBase, IAggregateColumn
    {
        public bool Visible { get; set; }

        public AggregateType Type { get; set; }

        public string Left { get; set; }

        public string Right { get; set; }

        public double Result { get; set; }
    }
}
