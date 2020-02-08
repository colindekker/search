namespace D3.Core.Search.Column.Models
{
    public class ColumnBase
        : IColumn
    {
        /// <summary>
        /// Gets or sets the internal (fixed) code.
        /// </summary>
        /// <value>
        /// The internal (fixed) column code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence. (defaults to 1)
        /// </value>
        public int Sequence { get; set; }
    }
}
