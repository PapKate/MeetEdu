using System.Linq.Expressions;
using System.Reflection;

namespace AppointMate
{
    /// <summary>
    /// Extension methods for <see cref="Expression"/>s
    /// </summary>
    public static class ExpressionExtensions
    {

        /// <summary>
        /// Used for analyzing an expression like x => x.Property and returning the
        /// <see cref="PropertyInfo"/> of the accessed property.
        /// </summary>
        /// <typeparam name="TSource">The type of the container of the property</typeparam>
        /// <typeparam name="TProperty">The type of the target property</typeparam>
        /// <param name="propertyExpression">The expression targeting the property</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
            => (PropertyInfo)GetMemberExpression(propertyExpression)!.Member;

        /// <summary>
        /// Used for analyzing an expression like () => SomeInstance.Property and returning the
        /// <see cref="PropertyInfo"/> of the accessed property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the target property</typeparam>
        /// <param name="propertyExpression">The expression targeting the property</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<TProperty>(this Expression<Func<TProperty>> propertyExpression)
            => (PropertyInfo)GetMemberExpression(propertyExpression)!.Member;


        /// <summary>
        /// Returns the <see cref="MemberExpression"/> from the specified <paramref name="expression"/>
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        private static MemberExpression? GetMemberExpression(Expression expression)
        {
            // Initialize the result
            var result = expression;

            // If the result is a lambda expression...
            if (result is LambdaExpression lambdaExpression)
                // Get its body
                result = lambdaExpression.Body;

            // If the expression is unary...
            if (result is UnaryExpression unary)
                // Get the expression of the operand
                result = unary.Operand;

            // If the expression is a method call...
            // NOTE: The method call expression is checked in order for retrieving properties
            //       when in form Property1.First().Property2
            if (result is MethodCallExpression methodCall)
            {
                // For every argument...
                foreach (var argument in methodCall.Arguments)
                    // If the argument is a member expression...
                    if (argument is not null && argument is MemberExpression)
                        // Return the argument
                        return (MemberExpression)argument;
            }

            // Return the type-casted expression
            return result as MemberExpression;
        }

    }
}
