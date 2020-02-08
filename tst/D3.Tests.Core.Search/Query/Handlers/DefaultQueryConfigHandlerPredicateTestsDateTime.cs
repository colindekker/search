namespace D3.Tests.Core.Search.Query.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using D3.Core.Search.Query.Handlers;
    using D3.Core.Search.Query.Models;
    using D3.Core.Search.Query.Values;
    using D3.Tests.Models.Queryable;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DefaultQueryConfigHandlerPredicateTestsDateTime
        : IClassFixture<DefaultQueryConfigHandlerFixture>
    {
        public DefaultQueryConfigHandlerPredicateTestsDateTime(DefaultQueryConfigHandlerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_Between()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.And,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-14", Compare = QueryPredicateValueCompare.GreaterThan });
            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-16", Compare = QueryPredicateValueCompare.LessThanOrEqual });

            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem { Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)},
                new TestItem { Name = "Lindsay", Id = 3, Start = new DateTime(2020, 1, 15)}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_NotEqual()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-15", Compare = QueryPredicateValueCompare.NotEqual });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem { Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)},
                new TestItem { Name = "Lindsay", Id = 3, Start = new DateTime(2020, 1, 15)}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(2, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_LessThanOrEqual()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-15", Compare = QueryPredicateValueCompare.LessThanOrEqual });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem { Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)},
                new TestItem { Name = "Lindsay", Id = 3, Start = new DateTime(2020, 1, 15)}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(2, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_LessThan()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-15", Compare = QueryPredicateValueCompare.LessThan });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem { Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_GreaterThanOrEqual()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-15", Compare = QueryPredicateValueCompare.GreaterThanOrEqual });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem { Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)},
                new TestItem { Name = "Lindsay", Id = 3, Start = new DateTime(2020, 1, 15)}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(2, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_GreaterThan()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-01-15", Compare = QueryPredicateValueCompare.GreaterThan });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem { Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem { Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)}
            };

            var result = await handler
                .HandleAsync<TestItem, QueryResult<TestItem>>(config, source.AsQueryable())
                .ConfigureAwait(false);

            Assert.NotNull(result);
            Assert.Equal(1, result.Total);
            Assert.NotEmpty(result.Payload);
        }

        [Fact]
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_DateTime_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Start",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "2020-02-01", Compare = QueryPredicateValueCompare.Equal });
            config.QueryBy.Add(p1);

            var source = new List<TestItem>
            {
                new TestItem {Name = "Colin", Id = 1, Start = new DateTime(2020, 1, 1)},
                new TestItem {Name = "Brendan", Id = 2, Start = new DateTime(2020, 2, 1)}
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