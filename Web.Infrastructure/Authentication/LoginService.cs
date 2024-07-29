using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Aplication.Modal;
using Web.Domain.Entities;

namespace Web.Infrastructure.Authentication
{
    public interface ILoginService
    {
        Task<string> Login(LoginModel model);
    }
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public LoginService(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> Login(LoginModel model)
        {
            //var result = await _signInManager.PasswordSignInAsync(model.UserName,model.Password, model.RememberMe, lockoutOnFailure: true);
            var user = await _userManager.FindByNameAsync(model.UserName);
            return await GenerateJwtToken(user ?? new ApplicationUser());
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var roles = await _userManager.GetRolesAsync(user);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role,string.Join(",", roles))
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"]!,
                audience: _configuration["Jwt:Audience"]!,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
