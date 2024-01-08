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
        /// <param name="user">User object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new Exception("Invalid email address");
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    throw new ArgumentNullException("Empty password");
                }

                var password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
                user.Password = password;
                await _userRepository.Create(user);
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
        public async Task<IActionResult> Update(int id, User user)
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
