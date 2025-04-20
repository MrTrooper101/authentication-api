using authentication_api.Common.Results;
using authentication_api.Domain.Entities;
using authentication_api.Features.Users.Commands;
using authentication_api.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace authentication_api.Features.Users.Handlers
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, OperationResult<bool>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public SetPasswordCommandHandler(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<OperationResult<bool>> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.SetPasswordDto;

                if (dto.Password != dto.ConfirmPassword)
                    return OperationResult<bool>.Failure("Passwords do not match");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == dto.Token, cancellationToken);

                if (user == null)
                    return OperationResult<bool>.Failure("Invalid or expired token");

                user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
                user.IsEmailConfirmed = true;
                user.EmailConfirmationToken = null;

                await _context.SaveChangesAsync(cancellationToken);

                return OperationResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex.Message);
            }
        }
    }
}
