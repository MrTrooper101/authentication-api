using authentication_api.Common.Results;
using authentication_api.Features.Users.DTOs;
using MediatR;
namespace authentication_api.Features.Users.Commands
{
    public class RegisterUserCommand : IRequest<OperationResult<Guid>>
    {
        public RegisterUserDto UserDto { get; set; }

        public RegisterUserCommand(RegisterUserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
