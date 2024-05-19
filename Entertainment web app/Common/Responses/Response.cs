namespace Entertainment_web_app.Common.Responses;


public class Response
{
    public string Status { get; set; } = null!;
    public int StatusCode { get; set; }
    public Error Error { get; set; } = null!;
}

public class Response<T> : Response
{
    public IEnumerable<T>? Data { get; set; }
}
