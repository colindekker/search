namespace D3.Core.Search.Query.Values
{
    public enum QueryPredicateValueCompare
    {
        /// <summary>
        /// Value of the predicate is equal to query value.
        /// </summary>
        Equal,

        /// <summary>
        /// Value of the predicate is not equal to query value.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Value of the predicate is contained in the query value.
        /// </summary>
        Contains,

        /// <summary>
        /// Query value starts with value of the predicate.
        /// </summary>
        StartsWith,

        /// <summary>
        /// Query value ends with value of the predicate.
        /// </summary>
        EndsWith,

        /// <summary>
        /// Query value is less than value of the predicate.
        /// </summary>
        LessThan,

        /// <summary>
        /// Query value is equal or less than value of the predicate.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Query value is greater than value of the predicate.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Query value is greater or less than value of the predicate.
        /// </summary>
        GreaterThanOrEqual
    }
}
