namespace authentication_api.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; }

        public string? EmailConfirmationToken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other,
        PreferNotToSay
    }

}
