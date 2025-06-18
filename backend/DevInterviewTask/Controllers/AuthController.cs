using AutoMapper;
using DevInterviewTask.Application.DTOs;
using DevInterviewTask.Domain.Users;
using DevInterviewTask.Infrastructure.Auth;
using DevInterviewTask.Infrastructure.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevInterviewTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, IAuthService authService, IUserService userService)
        {
            _mapper = mapper;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var isValid = await _authService.IsUserCredentialsValid(dto.Email, dto.Password);
            if (!isValid)
            {
                return BadRequest(new { message = "Invalid email or password."});
            }

            var user = await _userService.GetUserByEmailAsync(dto.Email);

            var jwt = await _authService.IssueJwt(user!, dto.RemeberMe);

            return Ok(new { token = jwt });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
        {
            var newUser = _mapper.Map<User>(dto);

            var existingUser = await _userService.GetUserByEmailAsync(dto.Email);

            if (existingUser is not null)
            {
                return BadRequest(new { message = "Email already taken." });
            }

            if (!string.Equals(dto.Password, dto.ConfirmPassword))
            {
                return BadRequest(new { message = "Password and confirm password don't match." });
            }

            newUser.PasswordHash = HashPassword(dto.Password);

            var userId = await _authService.RegisterUser(newUser);

            var user = await _userService.GetUserByIdAsync(userId);

            var jwt = await _authService.IssueJwt(user!, false);

            return Ok(new { token = jwt });
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] ExternalUserDto dto)
        {
            var existingUser = await _userService.GetUserByEmailAsync(dto.Email);

            // Ako se user registrirao s (istim) email + pass, nema veze... Preko Googla ce dobiti pristup bez pass jer je google trusted provider
            // inace: if (existingUser is not null && existingUser.IsExternal) else BadRequest("Email is already taken.)
            if (existingUser is not null)
            {
                var jwt = await _authService.IssueJwt(existingUser, true);

                return Ok(new { token = jwt });
            }

            // Ako user ne postoji s tim emailom registriraj ga (bez passworda) - kasnije moze postaviti password kroz user settings - ako stignem
            var newUser = _mapper.Map<User>(dto);

            var userId = await _authService.RegisterUser(newUser);

            var user = await _userService.GetUserByIdAsync(userId);

            var jwtt = await _authService.IssueJwt(user!, false);

            return Ok(new { token = jwtt });
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}