using authentication_api.Common.Results;
using authentication_api.Features.Users.DTOs;
using MediatR;

namespace authentication_api.Features.Users.Commands
{
    public class SetPasswordCommand : IRequest<OperationResult<bool>>
    {
        public SetPasswordDto SetPasswordDto;

        public SetPasswordCommand(SetPasswordDto setPasswordDto)
        {
            SetPasswordDto = setPasswordDto;
        }
    }
}
