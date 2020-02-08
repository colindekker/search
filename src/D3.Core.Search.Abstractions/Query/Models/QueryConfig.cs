namespace D3.Core.Search.Query.Models
{
    using System.Collections.Generic;

    public class QueryConfig
        : QueryConfigBase, IQueryConfig
    {
        public List<string> Includes { get; set; }
    }
}
