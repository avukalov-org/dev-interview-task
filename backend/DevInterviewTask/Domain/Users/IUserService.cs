namespace DevInterviewTask.Domain.Users
{
    public interface IUserService
    {
        Task<Guid> Create(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
    }
}
