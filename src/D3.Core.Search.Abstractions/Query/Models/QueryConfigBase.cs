namespace D3.Core.Search.Query.Models
{
    using System.Collections.Generic;
    using D3.Core.Search.Query;

    public class QueryConfigBase
        : IQueryConfigBase<QueryColumn, QueryOrder, QueryGroup, QueryPredicate, QueryPredicateValue>
    {
        public List<QueryColumn> Columns { get; set; }

        public List<QueryOrder> OrderBy { get; set; }

        public List<QueryGroup> GroupBy { get; set; }

        public List<QueryPredicate> QueryBy { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 30;

        public int Skip(int total)
        {
            if (Page <= 0)
            {
                throw new QueryException($"Page is {Page}. It cannot be <= 0");
            }

            var skip = (Page - 1) * Limit;

            if (skip > total)
            {
                throw new QueryException($"Trying to skip {skip} more than the total no. of items {total} ");
            }

            return skip;
        }

        public int Take(int total)
        {
            var take = Limit > total ? total : Limit;

            if (Skip(total) + take > total)
            {
                take = total - Skip(total);
            }

            return take;
        }
    }
}
