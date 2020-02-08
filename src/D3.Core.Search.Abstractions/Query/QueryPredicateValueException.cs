namespace D3.Core.Search.Query
{
    using System;

    /// <inheritdoc />
    public class QueryPredicateValueException
        : QueryPredicateException
    {
        public QueryPredicateValueException()
        {
        }

        public QueryPredicateValueException(string message)
            : base(message)
        {
        }

        public QueryPredicateValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
