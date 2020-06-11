using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.DepositCommands;
using WalletService.API.Application.Commands.IdentifiedCommands;
using WalletService.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Infrastructure.Idempotency;

namespace WalletService.API.Application.Commands.PaymentCommands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<CreateDepositCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreatePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IWalletRepository walletRepository,
        ILogger<CreateDepositCommandHandler> logger)
        {
            _walletRepository = walletRepository;
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePaymentCommand message, CancellationToken cancellationToken)
        {
            var payment = new Payment(message.Amount, message.PaymentMethodId);

            var wallet = await _walletRepository.GetByUserIdAsync(message.UserId);

            if (wallet == null)
            {
                return false;
            }

            payment.SetWalletId(wallet.Id);

            _logger.LogInformation("----- Creating Payment - deposit: {@Deposit}", payment);

            _paymentRepository.Add(payment);

            return await _paymentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }


    // Use for Idempotency in Command process
    public class CreatPaymentIdentifiedCommandHandler : IdentifiedCommandHandler<CreatePaymentCommand, bool>
    {
        public CreatPaymentIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreatePaymentCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating wallet.
        }
    }

}
