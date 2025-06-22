using DotNet.CleanArch.Template.Domain.Common.Interfaces;
using DotNet.CleanArch.Template.Domain.Common.Results;
using DotNet.CleanArch.Template.Domain.Entities;
using DotNet.CleanArch.Template.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace DotNet.CleanArch.Template.Infrastructure.Services.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<User> _signInManager;

        public IdentityService(
        UserManager<User> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }

        public async Task<Result<string>> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return Result<string>.Failure("Authentication failed");

            var jwtToken = await GenerateJwtToken(user);
            return Result<string>.Success(jwtToken);
        }

        public async Task<Result<bool>> AuthorizeAsync(int userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user != null ? Result<bool>.Success(true) : Result<bool>.Failure("User not found");
        }

        public async Task<Result<int>> CreateUserAsync(string firstName, string lastName, string email, string password)
        {
            var _UserName = $"{firstName.ToLowerInvariant()}.{lastName.ToLowerInvariant()}";
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = _UserName,
                Password = password,
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                NormalizedUserName = _userManager.NormalizeName(_UserName),
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded ? Result<int>.Success(user.Id) : Result<int>.Failure(result.Errors.Select(e => e.Description));

        }

        public async Task<Result> DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result.Failure("User not found");

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public async Task<Result<string>> GetUserNameAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result<string>.Failure("User not found");

            return Result<string>.Success(user.UserName);

        }

        public async Task<Result<bool>> IsInRoleAsync(int userId, string role)
        {
            if (string.IsNullOrEmpty(role))
                return Result<bool>.Failure("Role cannot be null or empty");

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return Result<bool>.Failure("User not found");

            var isInRole = await _userManager.IsInRoleAsync(user, role);

            if (!isInRole)
                return Result<bool>.Failure($"Role: {role} not found for this userId: {userId}");

            return Result<bool>.Success(isInRole);
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            // Adiciona roles do usuário
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.ValidIssuer,
                Audience = _jwtSettings.ValidAudience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
