using ApiTemplate.Domain.Repositories;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Infra.Data.Test.Utils;

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
    [InlineData(true, "admin", "admin@123")]
    [InlineData(false, "admin", "admin")]
    public void Should_Authenticate(bool shouldAuthenticate, string username, string password)
    {
        var authenticated = _userRepository.IsUserValid(username, password);

        if (shouldAuthenticate)
            Assert.True(authenticated);
        else
            Assert.False(authenticated);
    }

    [Theory]
    [InlineData(true, "admin", "admin@123")]
    [InlineData(false, "admin", "admin")]
    public async Task Should_Authenticate_Async(bool shouldAuthenticate, string username, string password)
    {
        var authenticated = await _userRepository.IsUserValidAsync(username, password);

        if (shouldAuthenticate)
            Assert.True(authenticated);
        else
            Assert.False(authenticated);
    }
}
