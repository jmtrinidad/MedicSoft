namespace MedicSoft.Web.Helpers
{
    using Data.Entities;
    using MedicSoft.Web.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserHelper
    {
        Task<IQueryable<UserViewModels>> GetAllUserAsync();

        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<IQueryable<UserViewModels>> GetRoleByUserAsync(List<User> users);

       Task<IdentityRole> GetRoleAsync(string id);

        Task<IdentityResult> RemoveUserToRolAsync(User user, string roleName);

        IEnumerable<SelectListItem> GetComboRoles();

    }
}
