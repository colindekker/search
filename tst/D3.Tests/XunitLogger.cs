namespace D3.Tests
{
    using System;
    using System.Diagnostics;
    using Microsoft.Extensions.Logging;

    public class XunitLogger<T> : ILogger<T>, IDisposable
    {
        public XunitLogger()
        {
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debug.WriteLine(state.ToString());
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}
