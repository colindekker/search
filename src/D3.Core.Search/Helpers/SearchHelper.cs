namespace D3.Core.Search.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    /// <summary>
    /// Search <see cref="Expression{TDelegate}"/> helpers.
    /// </summary>
    public static class SearchHelper
    {
        public static Expression<Func<T, TProperty>> CreatePropertySelectorExpression<T, TProperty>([NotNull] string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (typeof(T).GetProperty(propertyName) == null)
            {
                throw new SearchException($"Type '{typeof(T).Name}' does not define a property '{propertyName}'.");
            }

            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.Convert(Expression.PropertyOrField(parameter, propertyName), typeof(TProperty));
            return Expression.Lambda<Func<T, TProperty>>(body, parameter);
        }
    }
}
