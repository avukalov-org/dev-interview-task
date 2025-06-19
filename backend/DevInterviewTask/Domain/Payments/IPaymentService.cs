namespace DevInterviewTask.Domain.Payments
{
    public interface IPaymentService
    {
        Task AddAsync(UserPayment payment);

        Task<List<UserPayment>> FindByUserIdAsync(Guid userId);
    }
}