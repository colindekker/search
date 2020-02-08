namespace D3.Tests.Core.Search.Mapping.Query.Handlers
{
    using System.Reflection;
    using AutoMapper;
    using D3.Core.Search.Mapping.Query.Handlers;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using D3.Tests;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class MappingQueryConfigHandlerFixture
    {
        private static Assembly[] Assemblies { get; } =
        {
            typeof(MappingQueryConfigHandlerFixture).Assembly
        };

        public MappingQueryConfigHandlerFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddAutoMapper(Assemblies);

            serviceCollection
                .AddSingleton<IQueryConfigHandler<QueryConfig>, DefaultQueryConfigHandler>()
                .AddSingleton<IMappingQueryConfigHandler<QueryConfig>, MappingQueryConfigHandler>();

            serviceCollection
                .AddSingleton<ILogger<DefaultQueryConfigHandler>, XunitLogger<DefaultQueryConfigHandler>>()
                .AddSingleton<ILogger<MappingQueryConfigHandler>, XunitLogger<MappingQueryConfigHandler>>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}