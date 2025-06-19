using MediatR;

namespace DevInterviewTask.Application.Commands.Payments
{
    public class AddPaymentCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime PurchaseAt { get; set; } = DateTime.UtcNow;
    }
}