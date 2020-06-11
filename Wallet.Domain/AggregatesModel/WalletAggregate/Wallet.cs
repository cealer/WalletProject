using System;
using System.Collections.Generic;
using WalletService.Service.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.Exceptions;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Domain.AggregatesModel.WalletAggregate
{
    public class Wallet : Entity, IAggregateRoot
    {
        public Guid UserId { get; private set; }

        public decimal Balance { get; private set; }

        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method WalletAggrergateRoot.AddDeposit() which includes behaviour.
        private readonly List<Deposit> _deposits;
        public IReadOnlyCollection<Deposit> Deposits => _deposits;

        protected Wallet()
        {
            Balance = 0;
            _deposits = new List<Deposit>();
        }

        public Wallet(Guid userId) : this()
        {
            UserId = userId != Guid.Empty ? userId : throw new WalletDomainException($"UserId Invalid.");
        }

        public void IncreaseBalance(decimal amount)
        {
            ValidateAmount(amount);
            Balance += amount;
        }

        public void DecreaseBalance(decimal amount)
        {
            ValidateAmount(amount);

            if (amount > Balance)
            {
                throw new WalletDomainException("You don´t have enought Balance.");
            }

            Balance -= amount;

        }

        private void ValidateAmount(decimal amount)
        {
            if (amount < 1)
            {
                throw new WalletDomainException("Minimum amount is 1.");
            }
        }

        public void AddDeposit(Deposit deposit)
        {
            _deposits.Add(deposit);
            IncreaseBalance(deposit.Amount);
        }

    }
}
