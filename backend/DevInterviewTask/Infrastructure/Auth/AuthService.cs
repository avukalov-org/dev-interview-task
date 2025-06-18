using DevInterviewTask.Domain.Users;
using DevInterviewTask.Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevInterviewTask.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {

        private readonly IUserService _userService;
        private readonly JwtOptions _jwtOptions;

        public AuthService(IUserService userService, IOptions<JwtOptions> options)
        {
            _userService = userService;
            _jwtOptions = options.Value;
        }

        public async Task<Guid> RegisterUser(User user)
        {
            return await _userService.AddAsync(user);
        }

        public async Task<bool> IsUserCredentialsValid(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user is null)
            {
                return false;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return true;
            }

            return false;
        }

        public Task<string> IssueJwt(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user!.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, $"{user!.FirstName} {user!.LastName}"),
                new Claim(JwtRegisteredClaimNames.GivenName, user!.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user!.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user!.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwt);
        }
    }
}
