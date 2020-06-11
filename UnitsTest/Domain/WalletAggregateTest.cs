using System;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Service.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.Exceptions;
using Xunit;

namespace UnitsTest.Domain
{
    public class WalletServiceAggregateTest
    {
        [Fact]
        public void Create_Wallet_success()
        {
            //Arrange    
            var userId = Guid.NewGuid();

            //Act 
            var fakeWallet = new Wallet(userId);
            //Assert
            Assert.NotNull(fakeWallet);
        }

        [Fact]
        public void Increase_Wallet_success()
        {
            //Arrange    
            var userId = Guid.NewGuid();
            var amount = 20;
            var result = 20;

            //Act 
            var fakeWallet = new Wallet(userId);
            fakeWallet.IncreaseBalance(amount);

            //Assert
            Assert.Equal(result, fakeWallet.Balance);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        public void Increase_Wallet_failed(decimal amount)
        {
            //Arrange    
            var userId = Guid.NewGuid();
            var result = amount;

            //Act 
            var fakeWallet = new Wallet(userId);

            //Assert
            Assert.Throws<WalletDomainException>(() => fakeWallet.IncreaseBalance(amount));
        }

        [Fact]
        public void Decrease_Wallet_success()
        {
            //Arrange    
            var userId = Guid.NewGuid();
            var amountInitial = 20;
            var amount = 10;
            var result = amountInitial - amount;

            //Act 
            var fakeWallet = new Wallet(userId);
            fakeWallet.IncreaseBalance(amountInitial);
            fakeWallet.DecreaseBalance(amount);

            //Assert
            Assert.Equal(result, fakeWallet.Balance);
        }

        [Theory]
        [InlineData(20, 0.0)]
        [InlineData(20, -1.0)]
        [InlineData(20, 30)]
        public void Decrease_Wallet_failed(decimal amountInitial, decimal amount)
        {
            //Arrange    
            var userId = Guid.NewGuid();

            var result = amountInitial - amount;

            //Act 
            var fakeWallet = new Wallet(userId);
            fakeWallet.IncreaseBalance(amountInitial);

            //Assert
            Assert.Throws<WalletDomainException>(() => fakeWallet.DecreaseBalance(amount));
        }

        [Fact]
        public void Create_Deposit_success()
        {
            //Arrange    
            var userId = Guid.NewGuid();
            var initialAmount = 20;
            var deposit = 10;
            var result = initialAmount + deposit;

            //Act 
            var fakeWallet = new Wallet(userId);
            fakeWallet.IncreaseBalance(initialAmount);
            fakeWallet.AddDeposit(new Deposit(deposit));

            //Assert
            Assert.Equal(result, fakeWallet.Balance);
        }

        [Theory]
        [InlineData(20, 0.0)]
        [InlineData(20, -1.0)]
        public void Create_Deposit_failed(decimal initialAmount, decimal deposit)
        {
            //Arrange    
            var userId = Guid.NewGuid();
            var result = initialAmount + deposit;

            //Act 
            var fakeWallet = new Wallet(userId);
            fakeWallet.IncreaseBalance(initialAmount);

            //Assert
            Assert.Throws<WalletDomainException>(() => fakeWallet.AddDeposit(new Deposit(deposit)));
        }

    }
}
