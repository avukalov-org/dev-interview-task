using DevInterviewTask.Domain.Users;

namespace DevInterviewTask.Infrastructure.Auth
{
    public interface IAuthService
    {
        Task<Guid> RegisterUser(User user);
        Task<bool> IsUserCredentialsValid(string email, string password);
        Task<string> IssueJwt(User user, bool rememberMe);
    }
}
