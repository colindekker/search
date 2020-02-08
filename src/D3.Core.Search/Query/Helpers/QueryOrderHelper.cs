namespace D3.Core.Search.Query.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using D3.Core.Extensions;
    using D3.Core.Search.Query;
    using D3.Core.Search.Query.Models;
    using D3.Core.Search.Query.Values;

    public static class QueryOrderHelper
    {
        public static IQueryable<T> ApplyOrderBy<T>([NotNull] this IQueryable<T> source, [NotNull] QueryConfig config)
            where T : class
        {
            if (config.OrderBy?.Count > 0)
            {
                IOrderedQueryable<T> orderedQueryable = null;

                for (var i = 0; i < config.OrderBy.Count; i++)
                {
                    var info = config.OrderBy[i];
                    var code = info.ColumnCode.Capitalize();
                    if (info.SortDirection.Equals(QueryOrderDirection.Ascending))
                    {
                        orderedQueryable = i == 0
                            ? source.OrderBy(code)
                            : orderedQueryable.ThenBy(code);
                    }
                    else if (info.SortDirection.Equals(QueryOrderDirection.Descending))
                    {
                        orderedQueryable = i == 0
                            ? source.OrderByDescending(code)
                            : orderedQueryable.ThenByDescending(code);
                    }
                }

                return orderedQueryable;
            }

            return source;
        }

        public static IOrderedQueryable<T> OrderBy<T>([NotNull] this IQueryable<T> source, [NotNull] string property)
            where T : class
        {
            return ApplyOrder(source, property, nameof(OrderBy));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>([NotNull] this IQueryable<T> source, [NotNull] string property)
            where T : class
        {
            return ApplyOrder(source, property, nameof(OrderByDescending));
        }

        public static IOrderedQueryable<T> ThenBy<T>([NotNull] this IOrderedQueryable<T> source, [NotNull] string property)
            where T : class
        {
            return ApplyOrder(source, property, nameof(ThenBy));
        }

        public static IOrderedQueryable<T> ThenByDescending<T>([NotNull] this IOrderedQueryable<T> source, [NotNull] string property)
            where T : class
        {
            return ApplyOrder(source, property, nameof(ThenByDescending));
        }

        private static IOrderedQueryable<T> ApplyOrder<T>([NotNull] IQueryable<T> source, [NotNull] string property, [NotNull] string methodName)
            where T : class
        {
            var type = source.ElementType;
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            var pi = type.GetProperty(property);

            if (pi == null)
            {
                throw new QueryOrderException($"Type {type.Name} does not contain a property '{property}'.");
            }

            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;

            var delegateType = typeof(Func<,>).MakeGenericType(source.ElementType, type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable)
                .GetMethods()
                .Single(method => method.Name == methodName && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2)
                .MakeGenericMethod(source.ElementType, type)
                .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }
    }
}
