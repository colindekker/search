namespace D3.Core.Search.Query.Models
{
    using D3.Core.Search.Query.Values;

    public class QueryOrder
        : IQueryOrder
    {
        /// <summary>
        /// Gets or sets the column code.
        /// </summary>
        /// <value>
        /// The column code.
        /// </value>
        public string ColumnCode { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence. (defaults to 1)
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public QueryOrderDirection SortDirection { get; set; }
    }
}
