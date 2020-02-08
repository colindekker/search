namespace D3.Core.Search.Query
{
    using System;

    /// <inheritdoc />
    public class QueryOrderException
        : QueryException
    {
        public QueryOrderException()
        {
        }

        public QueryOrderException(string message)
            : base(message)
        {
        }

        public QueryOrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
