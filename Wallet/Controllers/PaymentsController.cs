using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.PaymentCommands;
using WalletService.API.Application.Queries.PaymentQueries;
using WalletService.API.Extensions;

namespace WalletService.API.Controllers
{
    public class PaymentsController : ApiController
    {
        private readonly IPaymentQueries _paymentQueries;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentQueries paymentQueries, ILogger<PaymentsController> logger)
        {
            _paymentQueries = paymentQueries ?? throw new ArgumentNullException(nameof(paymentQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("{userId:Guid}")]
        [HttpGet]
        [ProducesResponseType(typeof(PaymentViewModels), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetOrderAsync(Guid userId)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var order = await _paymentQueries.GetPaymentsByUserIdAsync(userId);

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateWalletAsync([FromBody] CreatePaymentCommand createPaymentCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createPaymentCommand.GetGenericTypeName(),
                nameof(createPaymentCommand.UserId),
                createPaymentCommand.UserId,
                createPaymentCommand);

            var result = await Mediator.Send(createPaymentCommand);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, new { error = "Deposit can't be created." });
        }

    }
}
