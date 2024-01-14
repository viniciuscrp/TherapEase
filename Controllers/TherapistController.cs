using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TherapEase.Data.Entities;
using TherapEase.Models.ViewModels;
using TherapEase.Repositories.Interfaces;

namespace TherapEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistRepository _userRepository;
        public TherapistController(ITherapistRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers an User
        /// </summary>
        /// <param name="registerViewModel">RegisterViewModel object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registerViewModel.Email))
                {
                    throw new ArgumentNullException("Invalid email address");
                }

                if (string.IsNullOrWhiteSpace(registerViewModel.Password))
                {
                    throw new ArgumentNullException("Empty password");
                }

                var alreadyExists = await _userRepository.Get(entity => entity.Email.Equals(registerViewModel.Email, StringComparison.CurrentCultureIgnoreCase));
                if (alreadyExists != null)
                {
                    return new BadRequestObjectResult(new { message = "Email address already in use" });
                }

                var therapist = new Therapist(registerViewModel.Name, registerViewModel.Email, registerViewModel.Password);
                await _userRepository.Create(therapist);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (typeof(Exception) == typeof(ArgumentNullException))
                {
                    return new BadRequestObjectResult(new { ex.Message });
                }

                return BadRequest("Something really bad happened :(");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("Id is invalid");
                }

                var entity = await _userRepository.Get(user => user.Id == id);
                if (entity == null)
                {
                    return NoContent();
                }

                return Ok(new UserViewModel(entity));
            }
            catch (Exception ex)
            {
                if (typeof(Exception) == typeof(ArgumentNullException))
                {
                    return new BadRequestObjectResult(new { ex.Message });
                }

                return BadRequest("Something realy bad happened :(");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Therapist user)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("Id is invalid");
                }

                var entity = await _userRepository.Get(user => user.Id == id);
                if (entity == null)
                {
                    throw new ArgumentNullException("User not found");
                }

                entity.Name = user.Name;
                await _userRepository.Update(id, entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (typeof(Exception) == typeof(ArgumentNullException))
                {
                    return new BadRequestObjectResult(new { ex.Message });
                }

                return BadRequest("Something realy bad happened :(");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("Id is invalid");
                }

                var entity = await _userRepository.Get(user => user.Id == id);
                if (entity == null)
                {
                    throw new ArgumentNullException("User not found");
                }

                await _userRepository.Delete(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (typeof(Exception) == typeof(ArgumentNullException))
                {
                    return new BadRequestObjectResult(new { ex.Message });
                }

                return BadRequest("Something realy bad happened :(");
            }
        }


    }
}
