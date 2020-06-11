using System;
using System.Threading.Tasks;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Domain.AggregatesModel.PaymentAggregate
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Payment Add(Payment payment);

        Payment Update(Payment payment);

        Task<Payment> GetAsync(Guid PaymentId);

        Task<Payment> GetByWalletIdAsync(Guid WalletId);

    }
}
