using AutoMapper;
using DevInterviewTask.Domain.Payments;
using MediatR;

namespace DevInterviewTask.Application.Commands.Payments
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Unit>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public AddPaymentCommandHandler(IMapper mapper, IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<Unit> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<UserPayment>(request);

            await _paymentService.AddAsync(payment);

            return Unit.Value;
        }
    }
}
