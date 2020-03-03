namespace D3.Tests.Core.Search.Query.Handlers
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

    public class DefaultQueryConfigHandlerPredicateTestsLong
        : IClassFixture<DefaultQueryConfigHandlerFixture>
    {
        public DefaultQueryConfigHandlerPredicateTestsLong(DefaultQueryConfigHandlerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        [Fact]
        public async Task Handler_Finds_Multiple_Items_By_Multiple_Predicates_For_Long_Property_With_Single_Or_Values_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig
            {
                QueryBy = new List<QueryPredicate>
                {
                    new QueryPredicate
                    {
                        ColumnCode = "ForeignId",
                        CompareWith = QueryPredicateConnective.Or,
                        Values = new List<QueryPredicateValue>
                        {
                            new QueryPredicateValue
                            {
                                Value = "100",
                                CompareWith = QueryPredicateConnective.Or,
                                CompareUsing = QueryPredicateComparison.Equal
                            }
                        }
                    },
                    new QueryPredicate
                    {
                        ColumnCode = "ForeignKey",
                        CompareWith = QueryPredicateConnective.Or,
                        Values = new List<QueryPredicateValue>
                        {
                            new QueryPredicateValue
                            {
                                Value = "200",
                                CompareWith = QueryPredicateConnective.Or,
                                CompareUsing = QueryPredicateComparison.Equal
                            }
                        }
                    }
                }
            };

            var source = new List<TestItem>
            {
                new TestItem
                {
                    Name = "Colin",
                    Id = 1,
                    ForeignId = 100,
                    ForeignKey = 100
                },
                new TestItem
                {
                    Name = "Brendan",
                    Id = 2,
                    ForeignId = 200,
                    ForeignKey = 200
                },
                new TestItem
                {
                    Name = "Drooling",
                    Id = 3,
                    ForeignId = 300,
                    ForeignKey = 300
                },
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(2, result.Total);
            Assert.NotEmpty(result.Payload);
            Assert.Contains("Colin", result.Payload.Select(p => p.Name));
            Assert.Contains("Brendan", result.Payload.Select(p => p.Name));
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_By_Single_Predicate_Nullable_Long_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "ForeignKey",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = null, CompareUsing = QueryPredicateComparison.Equal });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem {Name = "Colin", Id = 1, ForeignKey = null},
                new TestItem {Name = "Brendan", Id = 2, ForeignKey = 12346}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_By_Single_Predicate_Long_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "ForeignId",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "12345", CompareUsing = QueryPredicateComparison.Equal });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem {Name = "Colin", Id = 1, ForeignId = 12345},
                new TestItem {Name = "Brendan", Id = 2, ForeignId = 12346}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }
    }
}