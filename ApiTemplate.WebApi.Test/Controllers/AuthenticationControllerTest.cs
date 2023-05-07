namespace ApiTemplate.WebApi.Test.Controllers;

public class AuthenticationControllerTest : BaseControllerTest
{
    private readonly AuthenticationController _controller = new();

    [Theory]
    [InlineData("admin", "admin@123")]
    async public Task Login_WithValidCredentials_Success(string username, string password)
    {
        // Arrange
        var signInConfiguration = new SigningConfiguration();
        var tokenConfiguration = new TokenConfiguration
        {
            Audience = "test",
            Issuer = "test",
            Seconds = 20
        };

        var context = ContextUtil.GetContext();
        var reposity = new UserRepository(context);
        var service = new AuthenticateService(signInConfiguration, tokenConfiguration, reposity);

        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        var act = () => _controller.PostAsync(service, request);

        await act.Should()
            .NotThrowAsync();
    }

    [Theory]
    [InlineData("admin", "123456")]
    async public Task Login_WithValidCredentials_Fail(string username, string password)
    {
        // Arrange
        var signInConfiguration = new SigningConfiguration();
        var tokenConfiguration = new TokenConfiguration
        {
            Audience = "test",
            Issuer = "test",
            Seconds = 20
        };

        var context = ContextUtil.GetContext();
        var reposity = new UserRepository(context);
        var service = new AuthenticateService(signInConfiguration, tokenConfiguration, reposity);

        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        var act = () => _controller.PostAsync(service, request);
        await act.Should()
            .ThrowAsync<Exception>();
    }
}
