namespace DevInterviewTask.Domain.Users
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(UserEntity entity);

        Task<UserEntity?> FindByIdAsync(Guid id);

        Task<UserEntity?> FindByEmailAsync(string email);
    }
}