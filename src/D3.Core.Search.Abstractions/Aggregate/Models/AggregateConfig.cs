namespace D3.Core.Search.Aggregate.Models
{
    using System.Collections.Generic;

    public class AggregateConfig
        : IAggregateConfig<AggregateColumn>
    {
        public AggregateConfig()
        { }

        public AggregateConfig(IAggregateConfig<AggregateColumn> config)
        {
            Columns = config.Columns;
        }

        public List<AggregateColumn> Columns { get; set; }
    }
}
