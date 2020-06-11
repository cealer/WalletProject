using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WalletService.Service.Domain.Exceptions;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Service.Domain.AggregatesModel.PaymentAggregate
{
    public class Payment : Entity, IAggregateRoot
    {
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public int PaymentMethodId { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }

        public Guid GetWalletId => _walletId;
        private Guid _walletId;

        public Payment()
        {
        }

        public Payment(decimal amount, int paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
            Amount = amount > 0 ? amount : throw new WalletDomainException($"{nameof(Amount)} invalid.");
            Date = DateTime.Now;
        }

        public void SetWalletId(Guid id)
        {
            _walletId = id;
        }

    }
}
