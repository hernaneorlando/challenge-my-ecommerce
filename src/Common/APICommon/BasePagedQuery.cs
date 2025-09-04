using System.Text.Json.Serialization;
using Common.ORMCommon;
using Common.ORMCommon.Enums;
using MediatR;

namespace Common.APICommon;

public abstract record BasePagedQuery<TResponse> : IRequest<PaginatedResponse<TResponse>>
    where TResponse : class
{
    [JsonPropertyName("_page")]
    public int PageNumber { get; set; } = 1;

    [JsonPropertyName("_size")]
    public int PageSize { get; set; } = 10;

    [JsonPropertyName("_order")]
    public string OrderBy { get; set; } = string.Empty;

    [JsonIgnore]
    public List<FilterCriteria> Filters { get; set; } = [];

    public void AddFilter(string field, string value)
    {
        if (field.StartsWith("_min"))
        {
            Filters.Add(new FilterCriteria
            {
                Field = field[4..],
                Value = value,
                Operator = FilterOperator.GreaterThanOrEqual
            });
        }
        else if (field.StartsWith("_max"))
        {
            Filters.Add(new FilterCriteria
            {
                Field = field[4..],
                Value = value,
                Operator = FilterOperator.LessThanOrEqual
            });
        }
        else
        {
            Filters.Add(FilterCriteria.Parse(field, value));
        }
    }

    public Dictionary<string, string> SanitizeFilters(Dictionary<string, string> queryParams)
    {
        return queryParams
            .Where(f => !f.Key.StartsWith('_'))
            .Where(f =>
                f.Key != nameof(PageNumber) &&
                f.Key != nameof(PageSize) &&
                f.Key != nameof(OrderBy))
            .ToDictionary(f => f.Key, f => f.Value);
    }
}
