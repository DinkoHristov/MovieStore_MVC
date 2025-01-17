﻿using Microsoft.AspNetCore.Identity;
using MovieStore.Data.Models.Domain;
using MovieStore.Data.Models.DTO;
using MovieStore.Repositories.Abstract;
using System.Security.Claims;

namespace MovieStore.Repositories.ImplementedServices
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Username);

            if (user is null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>() { new Claim(ClaimTypes.Name, user.UserName) };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }

        public async Task LogoutAsync()
            => await signInManager.SignOutAsync();

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            
            if (userExists != null) 
            {
                status.StatusCode = 0;
                status.Message = "User already exists!";
                return status;
            }

            var newUser = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed!";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
                await userManager.AddToRoleAsync(newUser, model.Role);

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }
    }
}