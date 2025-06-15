namespace DevInterviewTask.Domain.Users
{
    public interface IUserRepository
    {
        Task<Guid> Create(UserEntity entity);

        Task<UserEntity?> FindById(Guid id);

        Task<UserEntity?> FindByEmail(string email);
    }
}