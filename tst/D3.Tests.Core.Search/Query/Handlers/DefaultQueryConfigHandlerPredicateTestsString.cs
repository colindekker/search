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

    public class DefaultQueryConfigHandlerPredicateTestsString
        : IClassFixture<DefaultQueryConfigHandlerFixture>
    {
        public DefaultQueryConfigHandlerPredicateTestsString(DefaultQueryConfigHandlerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_String_Sub_Collection_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Children.Name",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "ColinChild", CompareUsing = QueryPredicateComparison.Equal });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Children = new List<TestItemChild> { new TestItemChild { Id = 10, Name = "ColinChild" }}},
                new TestItem { Name = "Brendan", Id = 2, Children = new List<TestItemChild> { new TestItemChild { Id = 10, Name = "BrendanChild" }}}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_String_Sub_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Child.Name",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "ColinChild", CompareUsing = QueryPredicateComparison.Equal });
            config.QueryBy.Add(p1);

            var source = new List<TestItemChild>
            {
                new TestItemChild {Name = "Colin", Id = 1, Child = new TestItemChildChild { Id = 10, Name = "ColinChild"}},
                new TestItemChild {Name = "Brendan", Id = 2, Child = new TestItemChildChild { Id = 11, Name = "BrendanChild"}}
            };

            var result = await handler
                .HandleAsync<TestItemChild, QueryResult<TestItemChild>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_String_SubSub_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Child.Child.Name",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "ColinChildChild", CompareUsing = QueryPredicateComparison.Equal });
            config.QueryBy.Add(p1);

            var source = new List<TestItemChild>
            {
                new TestItemChild
                {
                    Id = 1,
                    Name = "Colin",
                    Child = new TestItemChildChild
                    {
                        Id = 10,
                        Name = "ColinChild",
                        Child = new TestItemChildChild
                        {
                            Id = 20,
                            Name = "ColinChildChild"
                        }
                    }
                },
                new TestItemChild
                {
                    Name = "Brendan",
                    Id = 2,
                    Child = new TestItemChildChild
                    {
                        Id = 11,
                        Name = "BrendanChild",
                        Child = new TestItemChildChild
                        {
                            Id = 21,
                            Name = "BrendanChildChild"
                        }
                    }
                }
            };

            var result = await handler
                .HandleAsync<TestItemChild, QueryResult<TestItemChild>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_String_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig {QueryBy = new List<QueryPredicate>()};

            var p1 = new QueryPredicate
            {
                ColumnCode = "Name",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue {Value = "Colin", CompareUsing = QueryPredicateComparison.Equal});
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem {Name = "Colin", Id = 1},
                new TestItem {Name = "Brendan", Id = 2}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_String_Property_Contains()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Name",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "oli", CompareUsing = QueryPredicateComparison.Contains });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem {Name = "Colin", Id = 1},
                new TestItem {Name = "Brendan", Id = 2}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Multiple_Items_In_Queryable_By_Single_Predicate_String_Property_Contains()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Name",
                CompareWith = QueryPredicateConnective.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "oli", CompareUsing = QueryPredicateComparison.Contains });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem {Name = "Colin", Id = 1},
                new TestItem {Name = "Brendan", Id = 2},
                new TestItem {Name = "Drooling", Id = 3},
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(2, result.Total);
            Assert.NotEmpty(result.Payload);
        }
    }
}