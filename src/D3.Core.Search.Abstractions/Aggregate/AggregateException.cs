namespace D3.Core.Search.Aggregate
{
    using System;

    /// <inheritdoc />
    public class AggregateException
        : SearchException
    {
        public AggregateException()
        {
        }

        public AggregateException(string message)
            : base(message)
        {
        }

        public AggregateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
