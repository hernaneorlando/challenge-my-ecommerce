using Common.ORMCommon.Enums;

namespace Common.ORMCommon;

public class FilterCriteria
{
    public string Field { get; set; } = string.Empty;
    public object Value { get; set; } = new object();
    public FilterOperator Operator { get; set; }

    public static FilterCriteria Parse(string field, string value)
    {
        if (value.StartsWith('*') && value.EndsWith('*'))
            return new FilterCriteria { Field = field, Value = value.Trim('*'), Operator = FilterOperator.Contains };

        if (value.StartsWith('*'))
            return new FilterCriteria { Field = field, Value = value.TrimStart('*'), Operator = FilterOperator.EndsWith };

        if (value.EndsWith('*'))
            return new FilterCriteria { Field = field, Value = value.TrimEnd('*'), Operator = FilterOperator.StartsWith };

        return new FilterCriteria { Field = field, Value = value, Operator = FilterOperator.Equals };
    }
}
