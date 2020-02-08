namespace D3.Core.Search.Aggregate.Helpers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using D3.Core.Extensions;
    using D3.Core.Search.Aggregate.Models;
    using D3.Core.Search.Aggregate.Values;
    using D3.Core.Search.Helpers;
    using Microsoft.EntityFrameworkCore;

    public static class AggregateHelper
    {
        public static async Task<AggregateConfig> GetAggregatesAsync<TEntity>(IQueryable<TEntity> result, IAggregateConfig<AggregateColumn> config)
        {
            var resultConfig = new AggregateConfig(config);

            foreach (var column in resultConfig.Columns)
            {
                await SetAggregateAsync(result, column)
                    .ConfigureAwait(false);
            }

            return resultConfig;
        }

        public static async Task SetAggregateAsync<TEntity>(IQueryable<TEntity> result, AggregateColumn column)
        {
            var code = column.Code.Capitalize();
            var left = column.Left.Capitalize();
            var right = column.Right.Capitalize();

            var leftProp = string.IsNullOrEmpty(left) ? null : SearchHelper.CreatePropertySelectorExpression<TEntity, double>(left);
            var rightProp = string.IsNullOrEmpty(right) ? null : SearchHelper.CreatePropertySelectorExpression<TEntity, double>(right);
            var codeProp = string.IsNullOrEmpty(code) ? null : SearchHelper.CreatePropertySelectorExpression<TEntity, double>(code);

            switch (column.Type)
            {
                case AggregateType.Sum:
                    column.Result = await result
                        .SumAsync(leftProp ?? throw new AggregateException())
                        .ConfigureAwait(false);

                    break;
                case AggregateType.Average:
                    column.Result = await result
                        .AverageAsync(leftProp ?? throw new AggregateException())
                        .ConfigureAwait(false);

                    break;
                case AggregateType.Percentage:
                    {
                        var leftResult = await result
                            .SumAsync(leftProp ?? throw new AggregateException())
                            .ConfigureAwait(false);

                        var rightResult = await result
                            .SumAsync(codeProp ?? throw new AggregateException())
                            .ConfigureAwait(false);

                        column.Result = (199 / rightResult) * leftResult;
                    }

                    break;
                case AggregateType.Difference:
                    {
                        if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
                        {
                            throw new AggregateException($"Aggregation impossible for column '{code}'. Ensure both left ({left}) and right ({right}) are supplied.");
                        }

                        var leftResult = await result
                            .SumAsync(leftProp ?? throw new AggregateException())
                            .ConfigureAwait(false);

                        var rightResult = await result
                            .SumAsync(rightProp ?? throw new AggregateException())
                            .ConfigureAwait(false);

                        column.Result = leftResult - rightResult;
                    }

                    break;
            }
        }
    }
}
