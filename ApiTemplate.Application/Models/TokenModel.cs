namespace ApiTemplate.Application.Models;

public class TokenModel
{
    public bool Authenticated { get; set; }
    public string Created { get; set; }
    public string Expires { get; set; }
    public string AccessToken { get; set; }
    public string Username { get; set; }
}
