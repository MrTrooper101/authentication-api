using authentication_api.Features.Users.Commands;
using authentication_api.Features.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace authentication_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var command = new RegisterUserCommand(registerUserDto);
            var userId = await _mediator.Send(command);

            return Ok(new
            {
                Message = "User registered successfully",
                UserId = userId
            });
        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto setPasswordDto)
        {
            var command = new SetPasswordCommand(setPasswordDto);
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { Errors = result.Errors });

            return Ok(new { Message = "Password set successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto);
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(new { Errors = result.Errors });

            return Ok(new { Token = result.Data });
        }
    }
}
