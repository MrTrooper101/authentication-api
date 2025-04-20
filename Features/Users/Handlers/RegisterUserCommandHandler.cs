using authentication_api.Application.Interfaces;
using authentication_api.Common.Results;
using authentication_api.Domain.Entities;
using authentication_api.Features.Users.Commands;
using authentication_api.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace authentication_api.Features.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<Guid>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public RegisterUserCommandHandler(ApplicationDbContext context, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<OperationResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UserDto;

            var emailToken = Guid.NewGuid().ToString();

            var user = new User
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                ContactNumber = dto.ContactNumber,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmationToken = emailToken,
                IsEmailConfirmed = false,
            };

            if (_context.Users.Any(u => u.Email == dto.Email))
            {
                return OperationResult<Guid>.Failure("Email already registered");
            }

            _context.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var encodedToken = WebUtility.UrlEncode(emailToken);

            var baseUrl = _configuration["Frontend:BaseUrl"];
            var verificationUrl = $"{baseUrl}/verify-email-token?token={encodedToken}";

            var subject = "Confirm your email";

            var body = $@"<p> Hi {user.FirstName},</p>
                        <p>Thanks for registering! Please confirm your email by clicking the link below</p>
                        <p><a href='{verificationUrl}'>Confirm Email</a></p>
                        <p>If you didn’t create this account, you can ignore this email.</p>";

            await _emailService.SendEmailAsync(user.Email, subject, body, cancellationToken);
            return OperationResult<Guid>.SuccessResult(user.Id);
        }
    }
}
