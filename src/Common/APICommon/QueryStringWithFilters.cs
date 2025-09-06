namespace Common.APICommon;

public class QueryStringWithFilters<TQuery, TResponse> : Dictionary<string, List<string>>
    where TQuery : BasePagedQuery<TResponse>, new()
    where TResponse : class
{
    private const string PageNumberKey = "_page";
    private const string PageSizeKey = "_size";
    private const string OrderByKey = "_order";

    public BasePagedQuery<TResponse> GetQuery()
    {
        var query = new TQuery();

        if (ContainsKey(PageNumberKey) || ContainsKey(nameof(BasePagedQuery<TResponse>.PageNumber)))
        {
            var value = SanitizeValues(this[PageNumberKey].FirstOrDefault()!);
            query.PageNumber = int.TryParse(value, out int page) ? page : 0;
            Remove(PageNumberKey);
        }

        if (ContainsKey(PageSizeKey) || ContainsKey(nameof(BasePagedQuery<TResponse>.PageSize)))
        {
            var value = SanitizeValues(this[PageSizeKey].FirstOrDefault()!);
            query.PageSize = int.TryParse(value, out int page) ? page : 0;
            Remove(PageSizeKey);
        }

        if (ContainsKey(OrderByKey) || ContainsKey(nameof(BasePagedQuery<TResponse>.OrderBy)))
        {
            query.OrderBy = SanitizeValues(this[OrderByKey].FirstOrDefault()!);
            Remove(OrderByKey);
        }

        foreach (var key in Keys)
        {
            var values = this[key];
            if (values.Count > 1)
                query.AddFilter(key, values.Select(SanitizeValues));
            else
                query.AddFilter(key, SanitizeValues(values.First()));
        }

        return query;
    }

    private static string SanitizeValues(string value)
    {
        // Just for swagger context
        return value
            .Replace("[\"", string.Empty)
            .Replace("\"]", string.Empty);
    }
}