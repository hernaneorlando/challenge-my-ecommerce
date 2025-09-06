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
            query.PageNumber = int.TryParse(this[PageNumberKey].FirstOrDefault(), out int page) ? page : 0;
            Remove(PageNumberKey);
        }

        if (ContainsKey(PageSizeKey) || ContainsKey(nameof(BasePagedQuery<TResponse>.PageSize)))
        {
            query.PageSize = int.TryParse(this[PageSizeKey].FirstOrDefault(), out int page) ? page : 0;
            Remove(PageSizeKey);
        }

        if (ContainsKey(OrderByKey) || ContainsKey(nameof(BasePagedQuery<TResponse>.OrderBy)))
        {
            query.OrderBy = this[OrderByKey].FirstOrDefault()!;
            Remove(OrderByKey);
        }

        foreach (var key in Keys)
        {
            var values = this[key];
            if (values.Count > 1)
                query.AddFilter(key, values);
            else
                query.AddFilter(key, values.First());
        }

        return query;
    }
}