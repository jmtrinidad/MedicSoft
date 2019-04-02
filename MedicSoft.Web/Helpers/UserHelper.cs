namespace MedicSoft.Web.Helpers
{
    using Data.Entities;
    using MedicSoft.Web.Data;
    using MedicSoft.Web.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext context;

        public UserHelper(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, DataContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IQueryable<UserViewModels>> GetAllUserAsync()
        {  
            var users= this.userManager.Users
                .Where(u => u.IsEnabled != false).ToList(); 
            return await this.GetRoleByUserAsync(users); 
        }

        public async Task<IQueryable<UserViewModels>> GetRoleByUserAsync(List<User> users)
        {
            List<UserViewModels> userViewModels = new List<UserViewModels>();

            if(users != null)
            {
                foreach (var user in users)
                {
                    var roles = await this.userManager.GetRolesAsync(user); 
                    if(roles.Count > 0)
                    {
                        var rol = Convert.ToString(roles[0]);
                        userViewModels.Add(new UserViewModels
                        {
                            Address = user.Address,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Rol = rol,

                        });
                    }   
                }
            } 
            
            return userViewModels.AsQueryable();
        }

        public async Task<IdentityRole> GetRoleAsync(string id)
        { 
            return  await  this.roleManager.FindByIdAsync(id);  
        }

        public async Task<IdentityResult> RemoveUserToRolAsync(User user,string roleName)
        {
            return await this.userManager.RemoveFromRoleAsync(user, roleName);
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = this.roleManager.Roles.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a rol...)",
                Value = "0"
            });

            return list;
        }
    }
}
