namespace D3.Core.Search.Query
{
    using System;

    /// <inheritdoc />
    public class QueryPredicateException
        : QueryException
    {
        public QueryPredicateException()
        {
        }

        public QueryPredicateException(string message)
            : base(message)
        {
        }

        public QueryPredicateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
