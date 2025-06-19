using AutoMapper;
using DevInterviewTask.Domain.Payments;

namespace DevInterviewTask.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(UserPayment payment)
        {
            var entity = _mapper.Map<UserPaymentEntity>(payment);

            await _paymentRepository.AddAsync(entity);
        }

        public async Task<List<UserPayment>> FindByUserIdAsync(Guid userId)
        {
            var entity = await _paymentRepository.FindByUserIdAsync(userId);

            return _mapper.Map<List<UserPayment>>(entity);
        }
    }
}