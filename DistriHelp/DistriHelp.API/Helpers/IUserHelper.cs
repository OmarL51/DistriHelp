﻿using DistriHelp.API.Data.Entities;
using DistriHelp.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DistriHelp.API.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);


        Task LogoutAsync();
    }
}