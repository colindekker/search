namespace D3.Core.Search.Query
{
    using System;

    /// <inheritdoc />
    public class QueryGroupException
        : QueryException
    {
        public QueryGroupException()
        {
        }

        public QueryGroupException(string message)
            : base(message)
        {
        }

        public QueryGroupException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
