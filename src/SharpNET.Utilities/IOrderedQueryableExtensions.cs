using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharpNET.Utilities
{
    public static class IQueryableExtensions
    {
        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, OrderByProperty orderbyProp, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
            MemberExpression property = Expression.PropertyOrField(param, orderbyProp.PropertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (orderbyProp.Descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        private static IOrderedQueryable<T> ProcessOrderBy<T>(IQueryable<T> source, string orderby, bool anotherLevel)
        {
            var props = new OrderByProperties(orderby);
            IOrderedQueryable<T> query = (IOrderedQueryable<T>)source;
            
            foreach (var item in props)
            {
                query = OrderingHelper(query, item, anotherLevel);
                anotherLevel = true;
            }
            return query;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderby)
        {

            return ProcessOrderBy(source, orderby, false);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string orderby)
        {
            return ProcessOrderBy(source, orderby, true);
        }

        private class OrderByProperties : List<OrderByProperty>
        {
            public OrderByProperties(string orderby)
            {
                string[] properties = orderby.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in properties)
                {
                    Add(new OrderByProperty(item));
                }
            }
        }

        private class OrderByProperty
        {
            public string PropertyName { get; private set; }
            public bool Descending { get; private set; }

            public OrderByProperty(string field)
            {
                string[] parts = field.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                PropertyName = parts[0];
                Descending = parts.Length > 1 && parts[1].ToLower().StartsWith("desc");
            }
        }
    }
}
