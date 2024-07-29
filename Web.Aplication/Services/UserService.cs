using Microsoft.AspNetCore.Identity;
using Web.Aplication.Modal;
using Web.Domain.Entities;

namespace Web.Aplication.Services
{
    public interface IUserService
    {
        Task<bool> Create(UserModel model);
        Task<bool> Update(string id, UserModel model);
        Task<bool> Delete(string id);


    }
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        public Task<bool> Create(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string id, UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
