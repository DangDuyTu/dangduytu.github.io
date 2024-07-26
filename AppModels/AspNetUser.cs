using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public ulong EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public ulong PhoneNumberConfirmed { get; set; }

    public ulong TwoFactorEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public ulong LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? FullName { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
