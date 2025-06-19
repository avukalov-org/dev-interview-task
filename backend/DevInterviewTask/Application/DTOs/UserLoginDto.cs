namespace DevInterviewTask.Application.DTOs
{
    public class UserLoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool RemeberMe { get; set; } = false;
    }
}