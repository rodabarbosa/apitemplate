using System;
using System.Threading.Tasks;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using Xunit;

namespace ApiTemplate.Tests.DataProjectTest;

public class UserRepositoryTest : IDisposable
{
    private readonly IUserRepository _userRepositry;
    private readonly ApiTemplateContext _context;

    public UserRepositoryTest()
    {
        _context = ContextUtil.GetContext();
        _userRepositry = new UserRepository(_context);
    }

    [Theory]
    [InlineData(true, "admin", "admin@123")]
    [InlineData(false, "admin", "admin")]
    public void Should_Authenticate(bool shouldAuthenticate, string username, string password)
    {
        bool authenticated = _userRepositry.IsUserValid(username, password);

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
        bool authenticated = await _userRepositry.IsUserValidAsync(username, password);

        if (shouldAuthenticate)
            Assert.True(authenticated);
        else
            Assert.False(authenticated);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}