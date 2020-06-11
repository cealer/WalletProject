using System;
using WalletService.Service.Domain.Exceptions;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Service.Domain.AggregatesModel.WalletService.Aggregate
{
    public class Deposit : Entity
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Deposit(decimal amount)
        {
            Amount = amount > 0 ? amount : throw new WalletDomainException("Invalid amount.");
            Date = DateTime.Now;
        }


    }
}
