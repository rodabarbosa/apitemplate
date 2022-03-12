namespace ApiTemplate.Shared.Models
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public object Error { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
    }
}
