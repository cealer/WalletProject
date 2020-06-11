using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WalletService.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.Events;

namespace WalletService.API.Application.DomainEventHandlers
{
    public class PaymentCreatedDomainEventHandler : INotificationHandler<PaymenCreatedDomainEvent>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILoggerFactory _logger;
        public PaymentCreatedDomainEventHandler(IWalletRepository walletRepository, ILoggerFactory logger)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PaymenCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(notification.UserId);
            wallet.DecreaseBalance(notification.Amount);
            await _walletRepository.UnitOfWork.SaveEntitiesAsync();
            _logger.CreateLogger("Payment Proceded succesfull.");
        }
    }
}
