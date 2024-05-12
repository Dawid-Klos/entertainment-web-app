namespace Entertainment_web_app.Common.Responses;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new Error(string.Empty, string.Empty);
}
