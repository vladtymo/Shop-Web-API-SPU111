using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountsService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task LoginAsync(LoginRequest model)
        {
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid login or password!", HttpStatusCode.BadRequest);

            await signInManager.SignInAsync(user, true);
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task RegisterAsync(RegisterRequest model)
        {
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Birthdate = model.Birthdate
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var message = string.Join(", ", result.Errors.Select(x => x.Description));
                
                throw new HttpException(message, HttpStatusCode.BadRequest);
            }
        }
    }
}
