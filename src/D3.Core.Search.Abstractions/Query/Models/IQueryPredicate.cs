namespace D3.Core.Search.Query.Models
{
    using System.Collections.Generic;
    using D3.Core.Search.Query.Values;

    public interface IQueryPredicate<TPredicateValue>
        where TPredicateValue : IQueryPredicateValue
    {
        /// <summary>
        /// Gets or sets the column code.
        /// </summary>
        /// <value>
        /// The column code.
        /// </value>
        string ColumnCode { get; set; }

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
        QueryPredicateCompare Compare { get; set; }

        IList<TPredicateValue> Values { get; set; }
    }
}
