using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace JustEatDemo.Common.General
{
    public static class PropertyExtentions
    {
        public static object GetPropValue<TObject>(TObject src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private static string GetPropertyName<TPropertySource>
             (Expression<Func<TPropertySource, object>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null,
               "Please provide a lambda expression like 'n => n.PropertyName'");

            if (memberExpression != null)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;

                return propertyInfo.Name;
            }

            return null;
        }
    }
}
