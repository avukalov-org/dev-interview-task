namespace DevInterviewTask.Domain.Users
{
    public interface IUserService
    {
        Task<Guid> AddAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
    }
}
