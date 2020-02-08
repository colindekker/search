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
        public async Task Handler_Finds_Single_Item_In_Queryable_By_Single_Predicate_Int_Property_Equal()
        {
            var handler = _serviceProvider
                .GetService<IQueryConfigHandler<QueryConfig>>();

            var config = new QueryConfig { QueryBy = new List<QueryPredicate>() };

            var p1 = new QueryPredicate
            {
                ColumnCode = "Id",
                Compare = QueryPredicateCompare.Or,
                Values = new List<QueryPredicateValue>()
            };

            p1.Values.Add(new QueryPredicateValue { Value = "1", Compare = QueryPredicateValueCompare.Equal });
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
    }
}