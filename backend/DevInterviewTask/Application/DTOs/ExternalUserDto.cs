namespace DevInterviewTask.Application.DTOs
{
    public class ExternalUserDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsExternal { get; set; } = false;
        public string ExternalProvider { get; set; } = default!;
    }
}