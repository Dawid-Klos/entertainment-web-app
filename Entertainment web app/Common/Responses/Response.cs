namespace Entertainment_web_app.Common.Responses;

public class Response<T>
{
    public string Status { get; set; } = null!;
    public int StatusCode { get; set; }
    public Error Error { get; set; } = null!;
    public IEnumerable<T>? Data { get; set; }
}
