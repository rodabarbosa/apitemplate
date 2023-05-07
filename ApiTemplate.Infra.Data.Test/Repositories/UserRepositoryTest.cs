namespace ApiTemplate.Infra.Data.Test.Repositories;

public class UserRepositoryTest
{
    private readonly IUserRepository _userRepository;

    public UserRepositoryTest()
    {
        var context = ContextUtil.GetContext();
        _userRepository = new UserRepository(context);
    }

    [Theory]
    [InlineData("admin", "admin@123")]
    public void Should_Authenticate(string username, string password)
    {
        var authenticated = _userRepository.IsUserValid(username, password);

        authenticated.Should()
            .BeTrue();
    }

    [Theory]
    [InlineData("admin", "admin")]
    public void Should_Not_Authenticate(string username, string password)
    {
        var authenticated = _userRepository.IsUserValid(username, password);

        authenticated.Should()
            .BeFalse();
    }

    [Theory]
    [InlineData("admin", "admin@123")]
    async public Task Should_Authenticate_Async(string username, string password)
    {
        var authenticated = await _userRepository.IsUserValidAsync(username, password);

        authenticated.Should()
            .BeTrue();
    }

    [Theory]
    [InlineData(false, "admin", "admin")]
    async public Task Should_Not_Authenticate_Async(bool shouldAuthenticate, string username, string password)
    {
        var authenticated = await _userRepository.IsUserValidAsync(username, password);

        authenticated.Should()
            .BeFalse();
    }
}
