using System.Linq.Expressions;
using Common.ORMCommon.Enums;

namespace Common.ORMCommon;

public static class QueryBuilder
{
    public static IQueryable<T> ApplyFilters<T>(
        this IQueryable<T> query,
        IEnumerable<FilterCriteria> filters)
    {
        foreach (var filter in filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filter.Field);
            var value = Expression.Constant(Convert.ChangeType(filter.Value, property.Type));

            Expression condition = filter.Operator switch
            {
                FilterOperator.Contains => Expression.Call(property,
                    typeof(string).GetMethod("Contains", [typeof(string)])!,
                    value),
                FilterOperator.StartsWith => Expression.Call(property,
                    typeof(string).GetMethod("StartsWith", [typeof(string)])!,
                    value),
                FilterOperator.EndsWith => Expression.Call(property,
                    typeof(string).GetMethod("EndsWith", [typeof(string)])!,
                    value),
                FilterOperator.GreaterThan => Expression.GreaterThan(property, value),
                FilterOperator.LessThan => Expression.LessThan(property, value),
                FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, value),
                FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, value),
                _ => Expression.Equal(property, value)
            };

            var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        return query;
    }
    
    public static IQueryable<T> ApplyOrder<T>(
        this IQueryable<T> query, 
        string orderByString)
    {
        if (string.IsNullOrEmpty(orderByString))
            return query;

        var orderCriteria = orderByString
            .Split(',')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x));

        var orderMethodName = "OrderBy";
        foreach (var criteria in orderCriteria)
        {
            var parts = criteria.Split(' ');
            var propertyName = parts[0];
            var descending = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            var parameter = Expression.Parameter(typeof(T), "x");
            
            var property = propertyName.Split('.')
                .Aggregate((Expression)parameter, Expression.Property);

            var lambda = Expression.Lambda(property, parameter);

            var method = typeof(Queryable)
                .GetMethods()
                .Where(m => m.Name == (descending ? orderMethodName + "Descending" : orderMethodName))
                .Single(m => m.GetParameters().Length == 2);

            var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);

            query = (IQueryable<T>)genericMethod.Invoke(null, [query, lambda])!;

            // Switch to ThenBy for subsequent order criteria
            orderMethodName = "ThenBy";
        }

        return query;
    }
}