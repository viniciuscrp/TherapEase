using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
