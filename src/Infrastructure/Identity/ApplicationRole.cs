﻿using Microsoft.AspNetCore.Identity;

namespace TransfusionAPI.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
