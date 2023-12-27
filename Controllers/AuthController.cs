using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TherapEase.Extensions;
using TherapEase.Helpers;
using TherapEase.Models.ViewModels;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        private readonly IAuthRepository _authRepository = authRepository;

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            try
            {
                string email = loginViewModel.Email;
                string password = loginViewModel.Password;
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentException("Invalid email");
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Invalid password");
                }

                var result = await _authRepository.Login(email, password);
                if (result == null)
                {
                    return Unauthorized();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return BadRequest();
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var accessToken = string.Empty;
                var refreshToken = string.Empty;
                if (!Request.Cookies.TryGetValue(Constants.Cookies.AccessToken.GetDescription(), out accessToken))
                {
                    return Unauthorized();
                }

                if (!Request.Cookies.TryGetValue(Constants.Cookies.RefreshToken.GetDescription(), out refreshToken))
                {
                    return Unauthorized();
                }

                if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized();
                }

                await _authRepository.RefreshAccessToken(accessToken, refreshToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return BadRequest();
        }
    }
}
