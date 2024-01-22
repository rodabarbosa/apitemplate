namespace ApiTemplate.Application.Test.Services;

public sealed class AuthenticationServiceTest : IDisposable
{
    private const string AudienceIssuer = "teste";
    private const int Expires = 10000;
    private readonly IAuthenticateService _authenticateService;
    private readonly ApiTemplateContext _context;

    public AuthenticationServiceTest()
    {
        // Arrange
        ISigningConfiguration signingConfiguration = new SigningConfiguration();
        ITokenConfiguration tokenConfiguration = new TokenConfiguration { Audience = AudienceIssuer, Issuer = AudienceIssuer, Seconds = Expires };

        _context = ContextUtil.GetContext();
        IUserRepository userRepository = new UserRepository(_context);

        _authenticateService = new AuthenticateService(signingConfiguration, tokenConfiguration, userRepository);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Theory]
    [InlineData("admin", "admin@123")]
    async public Task Should_Authenticate_User(string username, string password)
    {
        // Act
        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        var act = () => _authenticateService.Authenticate(request, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }

    [Theory]
    [InlineData("admin", "admin")]
    async public Task Should_Not_Authenticate_User(string username, string password)
    {
        // Act
        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        var act = () => _authenticateService.Authenticate(request, CancellationToken.None);

        await act.Should()
            .ThrowAsync<BadRequestException>();
    }
}
