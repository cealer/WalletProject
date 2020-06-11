using System;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using Xunit;

namespace UnitsTest.Domain
{
    public class PaymentAggregateTest
    {
        [Fact]
        public void Create_payment_success()
        {
            //Arrange    
            var walletId = Guid.NewGuid();
            var amount = 10;
            var paymentTypeId = PaymentMethod.Cash.Id;

            //Act 
            var fakePayment = new Payment(amount, paymentTypeId);
            fakePayment.SetWalletId(walletId);

            //Assert
            Assert.Equal(walletId, fakePayment.GetWalletId);
            Assert.Equal(amount, fakePayment.Amount);
            Assert.Equal(paymentTypeId, fakePayment.PaymentMethodId);
            Assert.NotEqual(new DateTime(), fakePayment.Date);

        }

    }
}
