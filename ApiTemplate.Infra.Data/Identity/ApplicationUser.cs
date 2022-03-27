using ApiTemplate.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApiTemplate.Infra.Data;

public sealed class ApplicationUser : IdentityUser, IUser
{
}