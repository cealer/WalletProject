using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.IdentifiedCommands;
using WalletService.API.Application.Commands.WalletCommands;
using WalletService.API.Application.Exceptions;
using WalletService.API.Infrastructure.Infrastructure.Services;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Infrastructure.Idempotency;

namespace WalletAPI.Application.Commands.WalletCommands
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, bool>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<CreateWalletCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateWalletCommandHandler(
            IWalletRepository walletRepository,
            ILogger<CreateWalletCommandHandler> logger)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateWalletCommand message, CancellationToken cancellationToken)
        {
            var wallet = new Wallet(message.UserId);

            _logger.LogInformation("----- Creating Wallet - wallet: {@Wallet}", wallet);

            var exist = await _walletRepository.GetByUserIdAsync(message.UserId);

            if (exist != null)
            {
                throw new WalletApplicationException("You can only have one wallet.");
            }

            _walletRepository.Add(wallet);

            return await _walletRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }


    // Use for Idempotency in Command process
    public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CreateWalletCommand, bool>
    {
        public CreateOrderIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateWalletCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating wallet.
        }
    }
}
