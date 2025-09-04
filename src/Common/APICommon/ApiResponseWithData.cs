namespace Common.APICommon;

public record ApiResponseWithData<T> : ApiResponse
    where T : class
{
    public T? Data { get; set; }
}
