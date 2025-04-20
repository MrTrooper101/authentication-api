namespace authentication_api.Features.Users.DTOs
{
    public class SetPasswordDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
