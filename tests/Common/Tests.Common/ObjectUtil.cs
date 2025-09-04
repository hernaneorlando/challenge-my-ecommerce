using System.Text.Json;

namespace Tests.Common;

public static class ObjectUtil
{
    public static T? Copy<T>(T source)
    {
        if (source == null)
        {
            return default;
        }

        var serialized = JsonSerializer.Serialize(source);
        return JsonSerializer.Deserialize<T>(serialized);
    }
}
