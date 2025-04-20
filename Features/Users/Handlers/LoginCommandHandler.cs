using authentication_api.Application.Interfaces;
using authentication_api.Common.Results;
using authentication_api.Domain.Entities;
using authentication_api.Features.Users.Commands;
using authentication_api.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace authentication_api.Features.Users.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginDto;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);

            if (user == null)
                return OperationResult<string>.Failure("Invalid email or password");

            if (!user.IsEmailConfirmed)
                return OperationResult<string>.Failure("Please confirm your email before logging in");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result != PasswordVerificationResult.Success)
                return OperationResult<string>.Failure("Invalid email or password");

            var token = _jwtService.GenerateToken(user);

            return OperationResult<string>.SuccessResult(token);
        }
    }
}
