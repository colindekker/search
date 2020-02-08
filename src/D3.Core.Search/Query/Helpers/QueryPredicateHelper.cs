namespace D3.Core.Search.Query.Helpers
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using D3.Core.Extensions;
    using D3.Core.Search.Query;
    using D3.Core.Search.Query.Models;
    using D3.Core.Search.Query.Values;
    using LinqKit;

    public static class QueryPredicateHelper
    {
        public static IQueryable<T> ApplyWhere<T>([NotNull] this IQueryable<T> source, [NotNull] QueryConfig config)
            where T : class
        {
            var result = source;
            if (config.QueryBy?.Count > 0)
            {
                var predicate = PredicateBuilder.New<T>();

                foreach (var queryPredicate in config.QueryBy)
                {
                    if (queryPredicate.Values == null)
                    {
                        throw new QueryPredicateException("Predicate cannot exist without values to compare");
                    }

                    var parameter = Expression.Parameter(typeof(T), "e");

                    Expression predicateExpression = null;

                    foreach (var queryPredicateValue in queryPredicate.Values)
                    {
                        var predicateValueExpression =
                            GetWhereValueComparison(typeof(T), parameter, queryPredicate, queryPredicateValue);

                        predicateExpression = predicateExpression == null
                            ? predicateValueExpression
                            : queryPredicate.Compare == QueryPredicateCompare.And
                                ? Expression.And(predicateExpression, predicateValueExpression)
                                : Expression.OrElse(predicateExpression, predicateValueExpression);
                    }

                    if (predicateExpression == null)
                    {
                        throw new QueryPredicateException("A valid Predicate could not be constructed.");
                    }

                    var e = Expression.Lambda<Func<T, bool>>(predicateExpression, parameter);

                    predicate = predicate.And(e);
                }

                result = source.Where(predicate);
            }

            return result;
        }

        /// <summary>
        /// Gets the where value comparison.
        /// https://stackoverflow.com/questions/16208214/construct-lambdaexpression-for-nested-property-from-string
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="predicateValue">The predicate value.</param>
        /// <returns></returns>
        /// <exception cref="QueryPredicateException">Predicate compares unknown property {code}.</exception>
        private static Expression GetWhereValueComparison(
            [NotNull] Type type,
            [NotNull] ParameterExpression parameter,
            [NotNull] IQueryPredicate<QueryPredicateValue> predicate,
            [NotNull] IQueryPredicateValue predicateValue)
        {
            if (predicate.ColumnCode.Contains('.'))
            {
                var code = predicate.ColumnCode.Split('.')[0];

                var property = type.GetProperty(code);

                if (property == null)
                {
                    throw new QueryPredicateException($"Predicate compares unknown property {code}.");
                }

                if (property.PropertyType.Implements<IEnumerable>())
                {
                    var c = Expression.PropertyOrField(parameter, predicate.ColumnCode.Split('.')[0]); // list property on parent
                    var a = property.PropertyType.GetGenericArguments()[0]; // list property on parent: item type
                    var p = Expression.Parameter(a, "c"); // comparison on list item type: left
                    var v = GetWherePropertyValueComparison(a, p, predicate, predicateValue); // comparison on list item type: right
                    var l = Expression.Lambda(v, p); // comparison on list item type

                    return Expression.Call(typeof(Enumerable), "Any", new[] { a }, c, l);
                }
                else
                {
                    Expression body = parameter;

                    // TODO
                    // make this a while looping through split on dot, adding to the body expression until the 
                    // current part is one of the datas types supported below for comparison (like string, datetime, enum etc.)
                    body = Expression.PropertyOrField(body, predicate.ColumnCode.Split('.')[0]);
                    return GetWherePropertyValueComparison(property.PropertyType, body, predicate, predicateValue);
                }
            }

            return GetWherePropertyValueComparison(type, parameter, predicate, predicateValue);
        }

        private static Expression GetWherePropertyValueComparison(
            [NotNull] Type type,
            [NotNull] Expression parameter,
            [NotNull] IQueryPredicate<QueryPredicateValue> predicate,
            [NotNull] IQueryPredicateValue predicateValue)
        {
            var code = predicate.ColumnCode.Split('.').Last().Capitalize();

            var property = type.GetProperty(code);

            if (property == null)
            {
                throw new QueryPredicateException($"Predicate compares unknown property {code}.");
            }

            var value = GetTypedValue(property, predicateValue.Value);

            switch (predicateValue.Compare)
            {
                case QueryPredicateValueCompare.Equal:
                    return Expression.Equal(Expression.Property(parameter, property), value);
                case QueryPredicateValueCompare.NotEqual:
                    return Expression.NotEqual(Expression.Property(parameter, property), value);
                case QueryPredicateValueCompare.LessThan:
                    return Expression.LessThan(Expression.Property(parameter, property), value);
                case QueryPredicateValueCompare.LessThanOrEqual:
                    return Expression.LessThanOrEqual(Expression.Property(parameter, property), value);
                case QueryPredicateValueCompare.GreaterThan:
                    return Expression.GreaterThan(Expression.Property(parameter, property), value);
                case QueryPredicateValueCompare.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(Expression.Property(parameter, property), value);
                case QueryPredicateValueCompare.StartsWith:
                case QueryPredicateValueCompare.EndsWith:
                case QueryPredicateValueCompare.Contains:
                    return property.PropertyType == typeof(string)
                        ? StringPropertyFilter(parameter, property, predicateValue.Value, predicateValue.Compare)
                        : throw new QueryPredicateValueException($"Property '{code}' on type {type.Name} should be of type string to use {predicateValue.Compare} as comparison type.");
                default:
                    throw new QueryPredicateValueException($"Predicate value uses unsupported compare type ({predicateValue.Compare}).");
            }
        }

        private static ConstantExpression GetTypedValue([NotNull] PropertyInfo property, [NotNull] string value)
        {
            if (property.PropertyType.BaseType == typeof(Enum))
            {
                var e = Enum.Parse(property.PropertyType, value);
                return Expression.Constant(e);
            }

            if (property.PropertyType == typeof(Guid))
            {
                return Expression.Constant(Guid.Parse(value));
            }

            if (property.PropertyType == typeof(Guid?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(Guid?))
                    : Expression.Constant(Guid.Parse(value), typeof(Guid?));
            }

            if (property.PropertyType == typeof(bool))
            {
                return Expression.Constant(bool.Parse(value));
            }

            if (property.PropertyType == typeof(bool?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(bool?))
                    : Expression.Constant(bool.Parse(value));
            }

            if (property.PropertyType == typeof(int))
            {
                return Expression.Constant(int.Parse(value));
            }

            if (property.PropertyType == typeof(int?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(int?))
                    : Expression.Constant(int.Parse(value), typeof(int?));
            }

            if (property.PropertyType == typeof(double))
            {
                return Expression.Constant(double.Parse(value));
            }

            if (property.PropertyType == typeof(double?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(double?))
                    : Expression.Constant(double.Parse(value), typeof(double?));
            }

            if (property.PropertyType == typeof(decimal))
            {
                return Expression.Constant(decimal.Parse(value));
            }

            if (property.PropertyType == typeof(decimal?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(decimal?))
                    : Expression.Constant(decimal.Parse(value), typeof(decimal?));
            }

            if (property.PropertyType == typeof(long))
            {
                return Expression.Constant(long.Parse(value));
            }

            if (property.PropertyType == typeof(long?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(long?))
                    : Expression.Constant(long.Parse(value), typeof(long?));
            }

            if (property.PropertyType == typeof(uint))
            {
                return Expression.Constant(uint.Parse(value));
            }

            if (property.PropertyType == typeof(uint?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(uint?))
                    : Expression.Constant(uint.Parse(value), typeof(uint?));
            }

            if (property.PropertyType == typeof(DateTime))
            {
                return Expression.Constant(DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind));
            }

            if (property.PropertyType == typeof(DateTime?))
            {
                return string.IsNullOrWhiteSpace(value)
                    ? Expression.Constant(null, typeof(DateTime?))
                    : Expression.Constant(DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind));
            }

            return Expression.Constant(value);
        }

        private static Expression StringPropertyFilter([NotNull] Expression parameter, [NotNull] PropertyInfo property, string value, QueryPredicateValueCompare type)
        {
            var method = typeof(string).GetMethod(type.ToString(), new[] { typeof(string) });
            if (method == null)
            {
                throw new QueryPredicateValueException($"Predicate value uses unsupported string compare type ({type}).");
            }

            return Expression.Call(Expression.Property(parameter, property), method, Expression.Constant(value));
        }
    }
}
