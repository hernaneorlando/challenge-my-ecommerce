namespace Common.ORMCommon;

public class PaginatedRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; } = string.Empty;
    public List<FilterCriteria> Filters { get; set; } = [];
}
