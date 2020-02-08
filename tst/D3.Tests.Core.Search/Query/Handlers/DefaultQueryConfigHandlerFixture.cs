namespace D3.Tests.Core.Search.Query.Handlers
{
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using D3.Tests;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class DefaultQueryConfigHandlerFixture
    {
        public DefaultQueryConfigHandlerFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IQueryConfigHandler<QueryConfig>, DefaultQueryConfigHandler>();
            serviceCollection
                .AddSingleton<ILogger<DefaultQueryConfigHandler>, XunitLogger<DefaultQueryConfigHandler>>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}