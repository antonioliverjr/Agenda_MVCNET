using Agenda_AspNet.Models.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Models
{
    public class Account : IAccount
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public Account(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AuthUser(string user, string password, bool remember)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, remember, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(string nome, string email, string password)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = nome,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
