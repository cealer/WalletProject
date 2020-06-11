using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.API.Controllers
{
    public class PaymentsController 
        //: ApiController
    {
        //private readonly IWalletQueries _walletQueries;
        //private readonly ILogger<WalletsController> _logger;

        //public WalletsController(IWalletQueries walletQueries, ILogger<WalletsController> logger)
        //{
        //    _walletQueries = walletQueries ?? throw new ArgumentNullException(nameof(walletQueries));
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        //[Route("{walletId:Guid}")]
        //[HttpGet]
        //[ProducesResponseType(typeof(WalletViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult> GetOrderAsync(Guid walletId)
        //{
        //    try
        //    {
        //        //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
        //        //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
        //        var order = await _walletQueries.GetWalletAsync(walletId);

        //        return Ok(order);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<bool>> CreateWalletAsync([FromBody] CreateWalletCommand createWalletCommand)
        //{
        //    _logger.LogInformation(
        //        "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
        //        createWalletCommand.GetGenericTypeName(),
        //        nameof(createWalletCommand.UserId),
        //        createWalletCommand.UserId,
        //        createWalletCommand);

        //    return await Mediator.Send(createWalletCommand);
        //}

    }
}
