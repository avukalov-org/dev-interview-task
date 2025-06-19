using DevInterviewTask.Application.Commands.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevInterviewTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] AddPaymentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}