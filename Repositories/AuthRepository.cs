using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TherapEase.Context;
using TherapEase.Data.Entities;
using TherapEase.Models;
using TherapEase.Models.ViewModels;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Repositories
{
    public class AuthRepository(ApiContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IAuthRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        protected readonly DbSet<User> _userDbSet = context.Set<User>();
        protected readonly DbSet<UserRefreshToken> _userRefreshTokens = context.Set<UserRefreshToken>();

        public async Task<UserViewModel> Login(string email, string password)
        {
            var user = await _userDbSet.Where(user => user.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            var result = BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);
            if (result)
            {
                var userViewModel = new UserViewModel(user);
                var authTokens = GenerateToken(email);
                var userRefreshToken = new UserRefreshToken
                {
                    Email = user.Email,
                    RefreshToken = authTokens.RefreshToken,
                };

                await HandleTokenAssignment(context, user.Id, authTokens, userRefreshToken);
                return userViewModel;
            }

            return null;
        }

        public async Task RefreshAccessToken(string accessToken, string refreshToken)
        {
            var claims = GetPrincipalFromExpiredToken(accessToken);
            if (claims == null)
            {
                throw new Exception("Invalid Access Token");
            }

            var email = claims.Claims.First().Subject.Name;
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Invalid Access Token");
            }

            var user = await _userDbSet.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                throw new Exception("Invalid Access Token");
            }

            var userRefreshToken = await _userRefreshTokens.FirstOrDefaultAsync(urf => urf.Email == email && urf.RefreshToken == refreshToken);
            if (userRefreshToken == null)
            {
                throw new Exception("Invalid Refresh Token");
            }

            var authTokens = GenerateToken(email); 
            userRefreshToken = new UserRefreshToken
            {
                Email = user.Email,
                RefreshToken = authTokens.RefreshToken,
            };
            
            await HandleTokenAssignment(context, user.Id, authTokens, userRefreshToken);
        }

        private AuthTokens GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new(ClaimTypes.Name, email)
                    }),
                    Expires = DateTime.Now.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new AuthTokens
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var randomNumber = new byte[32];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            return principal;
        }

        private async Task RevokeRefreshTokensAsync(string email)
        {
            try
            {
                var userRefreshTokens = await GetUserRefreshTokensAsync(email);
                if (userRefreshTokens.Count == 0)
                {
                    return;
                }

                _userRefreshTokens.RemoveRange(userRefreshTokens);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Failed to revoke old refresh tokens!");
            }
        }

        private async Task<List<UserRefreshToken>> GetUserRefreshTokensAsync(string email)
        {
            try
            {
                List<UserRefreshToken> userRefreshTokens = await _userRefreshTokens.Where(urt => urt.Email == email).ToListAsync();
                return userRefreshTokens;
            }
            catch
            {
                throw new Exception("Failed to retrieve user refresh tokens");
            }
        }

        private async Task HandleTokenAssignment(ApiContext context, int userId, AuthTokens authTokens, UserRefreshToken userRefreshToken)
        {
            await RevokeRefreshTokensAsync(userRefreshToken.Email);
            _userRefreshTokens.Add(userRefreshToken);
            await context.SaveChangesAsync();
            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", authTokens.AccessToken, new() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", authTokens.RefreshToken, new () { HttpOnly = true, SameSite = SameSiteMode.Strict });
            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-User-Id", userId.ToString(), new () { HttpOnly = true, SameSite = SameSiteMode.Strict });
        }
    }
}
