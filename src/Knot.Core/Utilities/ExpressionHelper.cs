using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Knot.Utilities
{
    /// <summary>
    /// Provides helper methods for working with expressions.
    /// </summary>
    internal static class ExpressionHelper
    {
        /// <summary>
        /// Gets the PropertyInfo from a member expression.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="expression">The member expression.</param>
        /// <returns>The PropertyInfo object.</returns>
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var memberExpression = GetMemberExpression(expression);
            if (memberExpression == null)
            {
                throw new ArgumentException("Expression must be a member expression.", nameof(expression));
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Expression must reference a property.", nameof(expression));
            }

            return propertyInfo;
        }

        /// <summary>
        /// Gets the property name from a member expression.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="expression">The member expression.</param>
        /// <returns>The property name.</returns>
        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            return GetPropertyInfo(expression).Name;
        }

        /// <summary>
        /// Extracts the member expression from an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The member expression if found; otherwise, null.</returns>
        private static MemberExpression? GetMemberExpression(Expression expression)
        {
            if (expression is MemberExpression memberExpression)
            {
                return memberExpression;
            }

            if (expression is LambdaExpression lambdaExpression)
            {
                if (lambdaExpression.Body is MemberExpression bodyMemberExpression)
                {
                    return bodyMemberExpression;
                }

                if (lambdaExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operandMemberExpression)
                {
                    return operandMemberExpression;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a getter function for a property.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>A function that gets the property value.</returns>
        public static Func<TSource, TProperty> CreateGetter<TSource, TProperty>(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            var parameter = Expression.Parameter(typeof(TSource), "obj");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda<Func<TSource, TProperty>>(property, parameter);
            return lambda.Compile();
        }

        /// <summary>
        /// Creates a setter action for a property.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>An action that sets the property value.</returns>
        public static Action<TSource, TProperty> CreateSetter<TSource, TProperty>(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            var parameter = Expression.Parameter(typeof(TSource), "obj");
            var valueParameter = Expression.Parameter(typeof(TProperty), "value");
            var property = Expression.Property(parameter, propertyInfo);
            var assign = Expression.Assign(property, valueParameter);
            var lambda = Expression.Lambda<Action<TSource, TProperty>>(assign, parameter, valueParameter);
            return lambda.Compile();
        }
    }
}
