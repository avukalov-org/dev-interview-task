namespace DevInterviewTask.Domain.Payments
{
    public class UserPaymentEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime PurchaseAt { get; set; } = DateTime.UtcNow;
    }
}
