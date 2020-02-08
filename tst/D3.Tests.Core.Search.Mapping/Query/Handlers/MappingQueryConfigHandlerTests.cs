namespace D3.Tests.Core.Search.Mapping.Query.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using D3.Core.Search.Query.Values;
    using D3.Tests.Models.Queryable;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MappingQueryConfigHandlerTests
        : IClassFixture<MappingQueryConfigHandlerFixture>
    {
        public MappingQueryConfigHandlerTests(MappingQueryConfigHandlerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_String_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IMappingQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig {QueryBy = new List<QueryPredicate>()};

            var p1 = new QueryPredicate
            {
                ColumnCode = "Name",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue {Value = "Dre", Compare = QueryPredicateValueCompare.Equal});
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem
                {
                    Name = "Dre",
                    Id = 1,
                    Children = new List<TestItemChild>
                    {
                        new TestItemChild
                        {
                            Id = 1,
                            Name = "Colin"
                        },
                        new TestItemChild
                        {
                            Id = 2,
                            Name = "Brendan"
                        },
                        new TestItemChild
                        {
                            Id = 3,
                            Name = "Lindsay"
                        }
                    }
                },
                new TestItem
                {
                    Name = "Jan",
                    Id = 2,
                    Children = new List<TestItemChild>
                    {
                        new TestItemChild
                        {
                            Id = 4,
                            Name = "Debby"
                        },
                        new TestItemChild
                        {
                            Id = 5,
                            Name = "Onno"
                        }
                    }
                }
            };

            var result = await handler
                .MapAsync<TestItem, TestItemMapped, QueryResult<TestItemMapped>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
            Assert.IsType<TestItemMapped>(result.Payload.First());
        }
    }
}