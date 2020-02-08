namespace D3.Tests.Core.Search.Mapping.EFCore.Query.Handlers
{
    using System;
    using D3.Core.Search.EFCore.Query.Handlers;
    using D3.Core.Search.Mapping.EFCore.Query.Handlers;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using D3.Tests;
    using D3.Tests.Core.Search.EFCore.Query.Handlers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class MappingEFCoreQueryConfigHandlerFixture
    {
        public MappingEFCoreQueryConfigHandlerFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddDbContext<TestDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            serviceCollection
                .AddSingleton<IQueryConfigHandler<QueryConfig>, DefaultQueryConfigHandler>()
                .AddSingleton<IDataQueryConfigHandler<QueryConfig>, EFCoreQueryConfigHandler>()
                .AddSingleton<IMappingDataQueryConfigHandler<QueryConfig>, MappingEFCoreQueryConfigHandler>();

            serviceCollection
                .AddSingleton<ILogger<DefaultQueryConfigHandler>, XunitLogger<DefaultQueryConfigHandler>>()
                .AddSingleton<ILogger<EFCoreQueryConfigHandler>, XunitLogger<EFCoreQueryConfigHandler>>()
                .AddSingleton<ILogger<MappingEFCoreQueryConfigHandler>, XunitLogger<MappingEFCoreQueryConfigHandler>>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}