namespace D3.Core.Search.Query
{
    using System;

    /// <inheritdoc />
    public class QueryException
        : SearchException
    {
        public QueryException()
        {
        }

        public QueryException(string message)
            : base(message)
        {
        }

        public QueryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
