using MediatR;
using System;

namespace WalletService.Service.Domain.Events
{
    public class PaymenCreatedDomainEvent : INotification
    {
        public Guid WalletId { get; }
        public decimal Amount { get; }

        public PaymenCreatedDomainEvent(Guid walletId, decimal amount)
        {
            WalletId = walletId;
            Amount = amount;
        }
    }
}
