using authentication_api.Common.Results;
using authentication_api.Features.Users.DTOs;
using MediatR;

namespace authentication_api.Features.Users.Commands
{
    public class LoginCommand : IRequest<OperationResult<string>>
    {
        public LoginDto LoginDto { get; set; }

        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
