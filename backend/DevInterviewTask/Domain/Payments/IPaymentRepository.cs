namespace DevInterviewTask.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task AddAsync(UserPaymentEntity entity);

        Task<List<UserPaymentEntity>> FindByUserIdAsync(Guid userId);
    }
}