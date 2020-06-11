using MediatR;
using System;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;

namespace WalletService.Service.Domain.Events
{
    public class PaymenCreatedDomainEvent : INotification
    {
        public Guid UserId { get; }
        public decimal Amount { get; }

        public PaymenCreatedDomainEvent(Guid userId, decimal amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
