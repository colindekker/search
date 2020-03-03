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

    public class DefaultQueryConfigHandlerPredicateTestsInt
        : IClassFixture<DefaultQueryConfigHandlerFixture>
    {
        public DefaultQueryConfigHandlerPredicateTestsInt(DefaultQueryConfigHandlerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        [Fact]
        public async Task Handler_Finds_Zero_Items_By_Single_Predicate_For_Int_Property_With_Multiple_And_Values_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig
            {
                QueryBy = new List<QueryPredicate>
                {
                    new QueryPredicate
                    {
                        ColumnCode = "Id",
                        CompareWith = QueryPredicateConnective.Or,
                        Values = new List<QueryPredicateValue>
                        {
                            new QueryPredicateValue
                            {
                                Value = "1",
                                CompareWith = QueryPredicateConnective.And,
                                CompareUsing = QueryPredicateComparison.Equal
                            },
                            new QueryPredicateValue
                            {
                                Value = "2",
                                CompareWith = QueryPredicateConnective.And,
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
                    Id = 1
                },
                new TestItem
                {
                    Name = "Brendan",
                    Id = 2
                },
                new TestItem
                {
                    Name = "Drooling",
                    Id = 3
                },
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(0, result.Total);
            Assert.Empty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Multiple_Items_By_Single_Predicate_For_Int_Property_With_Multiple_Or_Values_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig
            {
                QueryBy = new List<QueryPredicate>
                {
                    new QueryPredicate
                    {
                        ColumnCode = "Id",
                        CompareWith = QueryPredicateConnective.Or,
                        Values = new List<QueryPredicateValue>
                        {
                            new QueryPredicateValue
                            {
                                Value = "1",
                                CompareWith = QueryPredicateConnective.Or,
                                CompareUsing = QueryPredicateComparison.Equal
                            },
                            new QueryPredicateValue
                            {
                                Value = "2",
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
                    Id = 1
                },
                new TestItem
                {
                    Name = "Brendan",
                    Id = 2
                },
                new TestItem
                {
                    Name = "Drooling",
                    Id = 3
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
        public async Task Handler_Finds_Single_Item_By_Single_Predicate_For_Int_Property_With_Multiple_Or_Values_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig
            {
                QueryBy = new List<QueryPredicate>
                {
                    new QueryPredicate
                    {
                        ColumnCode = "Id",
                        CompareWith = QueryPredicateConnective.Or,
                        Values = new List<QueryPredicateValue>
                        {
                            new QueryPredicateValue
                            {
                                Value = "1",
                                CompareWith = QueryPredicateConnective.Or,
                                CompareUsing = QueryPredicateComparison.Equal
                            },
                            new QueryPredicateValue
                            {
                                Value = "5",
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
                    Id = 1
                },
                new TestItem
                {
                    Name = "Brendan",
                    Id = 2
                },
                new TestItem
                {
                    Name = "Drooling",
                    Id = 3
                },
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_By_Single_Predicate_For_Int_Property_With_Single_Or_Value_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig
            {
                QueryBy = new List<QueryPredicate>
                {
                    new QueryPredicate
                    {
                        ColumnCode = "Id",
                        CompareWith = QueryPredicateConnective.Or,
                        Values = new List<QueryPredicateValue>
                        {
                            new QueryPredicateValue
                            {
                                Value = "1",
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
                    Id = 1
                },
                new TestItem
                {
                    Name = "Brendan",
                    Id = 2
                },
                new TestItem
                {
                    Name = "Drooling",
                    Id = 3
                },
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