using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DXP.SmartConnectPickup.Common.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<TResult> ApplySortFilter<T, TResult>(this IQueryable<T> query, string columnName)
        {
            var type = typeof(T);
            var property = type.GetProperty(columnName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<TResult>(resultExp);
        }

        public static IQueryable<TEntity> OrderDynamicBy<TEntity>(this IQueryable<TEntity> source, string sortExpression)
        {
            var type = typeof(TEntity);

            // Remember that for ascending order GridView just returns the column name and
            // for descending it returns column name followed by DESC keyword
            // Therefore we need to examine the sortExpression and separate out Column Name and
            // order (ASC/DESC)

            string[] expressionParts = sortExpression.Split(' '); // Assuming sortExpression is like [ColumnName DESC] or [ColumnName]
            string orderByProperty = expressionParts[0];
            string methodName = "OrderBy";

            if (expressionParts.Length > 1 && expressionParts[1].Equals("DESC", StringComparison.OrdinalIgnoreCase))
            {
                methodName = "OrderByDescending"; // Add sort direction at the end of Method name
            }

            MethodCallExpression resultExp;
            if (!orderByProperty.Contains("."))
            {
                var property = type.GetProperty(orderByProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var parameter = Expression.Parameter(type);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { type, property.PropertyType },
                                            source.Expression, Expression.Quote(orderByExp));
            }
            else
            {
                Type relationType = type.GetProperty(orderByProperty.Split('.')[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;
                PropertyInfo relationProperty = type.GetProperty(orderByProperty.Split('.')[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo relationProperty2 = relationType.GetProperty(orderByProperty.Split('.')[1], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var parameter = Expression.Parameter(type);
                var propertyAccess = Expression.MakeMemberAccess(parameter, relationProperty);
                var propertyAccess2 = Expression.MakeMemberAccess(propertyAccess, relationProperty2);
                var orderByExp = Expression.Lambda(propertyAccess2, parameter);
                resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { type, relationProperty2.PropertyType },
                                            source.Expression, Expression.Quote(orderByExp));
            }

            return source.Provider.CreateQuery<TEntity>(resultExp);
        }
    }
}
