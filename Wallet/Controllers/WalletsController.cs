using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.DepositCommands;
using WalletService.API.Application.Commands.WalletCommands;
using WalletService.API.Application.Queries;
using WalletService.API.Extensions;

namespace WalletService.API.Controllers
{
    public class WalletsController : ApiController
    {
        private readonly IWalletQueries _walletQueries;
        private readonly ILogger<WalletsController> _logger;

        public WalletsController(IWalletQueries walletQueries, ILogger<WalletsController> logger)
        {
            _walletQueries = walletQueries ?? throw new ArgumentNullException(nameof(walletQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(WalletViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetByUserIdAsync(Guid userId)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var order = await _walletQueries.GetWalletByUserIdAsync(userId);

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalletAsync([FromBody] CreateWalletCommand createWalletCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createWalletCommand.GetGenericTypeName(),
                nameof(createWalletCommand.UserId),
                createWalletCommand.UserId,
                createWalletCommand);

            var commandResult = await Mediator.Send(createWalletCommand);

            if (!commandResult)
            {
                return StatusCode(500, new { error = "Wallet can't be created." });
            }

            return Ok();

        }

        [HttpPost("deposit")]
        public async Task<ActionResult<bool>> CreateDepositAsync([FromBody] CreateDepositCommand createWalletCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createWalletCommand.GetGenericTypeName(),
                nameof(createWalletCommand.UserId),
                createWalletCommand.UserId,
                createWalletCommand);

            var result = await Mediator.Send(createWalletCommand);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, new { error = "Deposit can't be created." });
        }

    }
}
