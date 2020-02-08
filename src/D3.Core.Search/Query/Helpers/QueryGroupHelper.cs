namespace D3.Core.Search.Query.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using D3.Core.Search.Query;
    using D3.Core.Search.Query.Models;

    public static class QueryGroupHelper
    {
        public static EnumerableQuery ApplyGroupBy<T>([NotNull] this IOrderedQueryable<T> source, [NotNull] QueryConfig config)
            where T : class
        {
            if (config.GroupBy != null)
            {
                var info = config.GroupBy[0];

                return source.GroupBy(info.ColumnCode);
            }

            return (EnumerableQuery)source;
        }

        public static EnumerableQuery GroupBy([NotNull] this IOrderedQueryable source, [NotNull] string property)
        {
            return ApplyGroup(source, property, nameof(GroupBy));
        }

        private static EnumerableQuery ApplyGroup([NotNull] IQueryable source, [NotNull] string property, [NotNull] string methodName)
        {
            var props = property.Split('.');
            var type = source.ElementType;
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (var prop in props)
            {
                var pi = type.GetProperty(prop);

                if (pi == null)
                {
                    throw new QueryGroupException($"Type {type.Name} does not contain a property '{property}'.");
                }

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(source.ElementType, type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable)
                .GetMethods()
                .Single(method => method.Name == methodName && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2)
                .MakeGenericMethod(source.ElementType, type)
                .Invoke(null, new object[] { source, lambda });

            return (EnumerableQuery)result;
        }
    }
}
