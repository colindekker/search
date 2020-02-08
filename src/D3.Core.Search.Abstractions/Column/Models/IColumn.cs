namespace D3.Core.Search.Column.Models
{
    public interface IColumn
    {
        /// <summary>
        /// Gets or sets the internal (fixed) code.
        /// </summary>
        /// <value>
        /// The internal (fixed) column code.
        /// </value>
        string Code { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence. (defaults to 1)
        /// </value>
        int Sequence { get; set; }
    }
}
