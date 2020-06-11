using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.IdentifiedCommands;
using WalletService.API.Application.Exceptions;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Infrastructure.Idempotency;

namespace WalletService.API.Application.Commands.DepositCommands
{
    public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, bool>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<CreateDepositCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateDepositCommandHandler(
            IWalletRepository walletRepository,
            ILogger<CreateDepositCommandHandler> logger)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateDepositCommand message, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(message.UserId);

            if (wallet == null)
            {
                throw new WalletApplicationException("Wallet not found.");
            }

            wallet.AddDeposit(new Deposit(message.Amount));
            _logger.LogInformation("----- Creating Deposit - deposit: {@Deposit}", wallet);

            _walletRepository.Update(wallet);

            return await _walletRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }


    // Use for Idempotency in Command process
    public class CreatDepositIdentifiedCommandHandler : IdentifiedCommandHandler<CreateDepositCommand, bool>
    {
        public CreatDepositIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateDepositCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating wallet.
        }
    }

}
