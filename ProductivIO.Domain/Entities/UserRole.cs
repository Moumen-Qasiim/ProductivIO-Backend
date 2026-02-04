using Microsoft.AspNetCore.Identity;

namespace ProductivIO.Domain.Entities;

/// <summary>
/// Domain entity representing a user role in the system.
/// Extends ASP.NET Core Identity Role with Guid as the key.
/// </summary>
public class UserRole : IdentityRole<Guid>
{
}
