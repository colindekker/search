namespace D3.Tests.Core.Search.Mapping.EFCore.Query.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using D3.Core.Search.Query.Values;
    using D3.Tests.Core.Search.EFCore.Query.Handlers;
    using D3.Tests.Models.Entities;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MappingEFCoreQueryConfigHandlerTests
        : IClassFixture<MappingEFCoreQueryConfigHandlerFixture>
    {
        public MappingEFCoreQueryConfigHandlerTests(MappingEFCoreQueryConfigHandlerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        [Fact]
        public async Task Handler_Finds_Single_Item_In_DbContext_By_Single_Predicate_String_Property_Equal()
        {
            var context = _serviceProvider
                .GetService<TestDbContext>();

            var handler = _serviceProvider
                .GetService<IDataQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Name",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "June", Compare = QueryPredicateValueCompare.Equal });
            config.QueryBy.Add(p1);

            var source = new List<Parent>
            {
                new Parent { Name = "Dre", Id = 1 },
                new Parent { Name = "June", Id = 2 }
            };

            await context.Parents
                .AddRangeAsync(source)
                .ConfigureAwait(false);

            await context
                .SaveChangesAsync(true)
                .ConfigureAwait(false);

            var result = await handler
                .HandleAsync<Parent, QueryResult<Parent>>(config, context)
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }
    }
}