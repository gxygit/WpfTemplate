using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ViewModel.Base
{
    /// <summary>
    /// a helper for expressions
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        /// compiles an expression and get the functions return value
        /// </summary>
        /// <typeparam name="T">the type of return value</typeparam>
        /// <param name="lamba"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lamba)
        {
            return lamba.Compile().Invoke();
        }
        /// <summary>
        /// set the underlying properties value to the given value from an expression that contains the property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lamba"></param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lamba, T value)
        {
            //converts a lamba ()=>some.property ,to some.property
            var expression = (lamba as LambdaExpression).Body as MemberExpression;

            //get the property information so we can set it
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();
            propertyInfo.SetValue(target, value, null);
        }
    }
}
