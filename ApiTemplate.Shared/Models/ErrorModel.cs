namespace ApiTemplate.Shared.Models
{
    public sealed class ErrorModel
    {
        public int Code { get; init; }
        public object Error { get; init; }
        public string Exception { get; init; }
        public string StackTrace { get; init; }
    }
}
