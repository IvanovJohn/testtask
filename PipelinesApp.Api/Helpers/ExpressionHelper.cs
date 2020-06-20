namespace PipelinesApp.Api.Helpers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression,
                                                       Expression<Func<T, bool>> rightExpression)
        {
            var invocationExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);
            var andExpression = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(leftExpression.Body, invocationExpression),
                leftExpression.Parameters);

            return andExpression;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExpression,
                                                      Expression<Func<T, bool>> rightExpression)
        {
            var invocationExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);
            var expression = Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(leftExpression.Body, invocationExpression),
                leftExpression.Parameters);

            return expression;
        }

        private static MemberExpression NestedExpressionProperty(Expression expression, string propertyName)
        {
            string[] parts = propertyName.Split('.');
            int partsL = parts.Length;

            return (partsL > 1)
                ?
                Expression.Property(
                    NestedExpressionProperty(
                        expression,
                        parts.Take(partsL - 1)
                            .Aggregate((a, i) => a + "." + i)),
                    parts[partsL - 1])
                :
                Expression.Property(expression, propertyName);
        }
    }
}