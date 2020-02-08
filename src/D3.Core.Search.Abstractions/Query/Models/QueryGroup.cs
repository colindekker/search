namespace D3.Core.Search.Query.Models
{
    public class QueryGroup
        : IQueryGroup
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
    }
}
