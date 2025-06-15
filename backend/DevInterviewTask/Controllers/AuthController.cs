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
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            // TODO: Check if user exists and pasword match
            var isValid = await _authService.IsUserCredentialsValid(request.Email, request.Password);
            if (!isValid)
            {
                return BadRequest("Email or password is invalid.");
            }

            var user = await _userService.GetUserByEmail(request.Email);

            var jwt = await _authService.IssueJwt(user!, request.RemeberMe);

            return Ok(new { token = jwt });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
        {
            // Validate RegisterRequst
            var newUser = _mapper.Map<User>(dto);

            var existingUser = await _userService.GetUserByEmail(dto.Email);

            if (existingUser is not null)
            {
                return BadRequest("Email already taken.");
            }

            if (!string.Equals(dto.Password, dto.ConfirmPassword))
            {
                return BadRequest("Password and ConfirmPassword don't match.");
            }

            newUser.PasswordHash = HashPassword(dto.Password);

            var userId = await _authService.RegisterUser(newUser);

            var user = await _userService.GetUserById(userId);

            var jwt = await _authService.IssueJwt(user!, false);

            return Ok(new { token = jwt });
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}