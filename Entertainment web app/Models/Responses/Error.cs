namespace Entertainment_web_app.Models
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty);
    }
}
