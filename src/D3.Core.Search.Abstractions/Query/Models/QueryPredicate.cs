namespace D3.Core.Search.Query.Models
{
    using System.Collections.Generic;
    using D3.Core.Search.Query.Values;

    public class QueryPredicate
        : IQueryPredicate<QueryPredicateValue>
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
        /// Gets or sets the check.
        /// </summary>
        /// <value>
        /// The boolean comparison type. (defaults to PredicateValueCompareAs.And)
        /// </value>
        public QueryPredicateCompare Compare { get; set; }

        public IList<QueryPredicateValue> Values { get; set; } = new List<QueryPredicateValue>();
    }
}
