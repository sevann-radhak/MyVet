using Microsoft.AspNetCore.Identity;
using MyVet.Web.Data.Entities;
using MyVet.Web.Models;
using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
    public interface IUserHelper
    {
        Task AddUserToRoleAsync(User user, string roleName);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task<bool> DeleteUserAsync(string email);

        Task<User> GetUserByEmailAsync(string email);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

    }
}
