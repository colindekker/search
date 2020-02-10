namespace D3.Core.Search.Query.Models
{
    using D3.Core.Search.Query.Values;

    public interface IQueryPredicateValue
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value to compare against input from (Column:current value).
        /// </value>
        string Value { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence. (defaults to 1)
        /// </value>
        int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the check.
        /// </summary>
        /// <value>
        /// The boolean comparison type. (defaults to PredicateValueCompareAs.And)
        /// </value>
        QueryPredicateConnective CompareWith { get; set; }

        /// <summary>
        /// Gets or sets the check.
        /// </summary>
        /// <value>
        /// The type of comparison. (defaults to PredicateValueCompare.Equal)
        /// </value>
        QueryPredicateComparison CompareUsing { get; set; }

        /// <summary>
        /// Gets or sets the formula. Can be used for say RegEx filters
        /// </summary>
        /// <value>
        /// The formula.
        /// </value>
        string Formula { get; set; }
    }
}
