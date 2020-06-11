using System;
using System.Threading.Tasks;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Domain.AggregatesModel.WalletService.Aggregate
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet Add(Wallet wallet);

        Wallet Update(Wallet wallet);

        Task<Wallet> GetAsync(Guid WalletId);

        Task<Wallet> GetByUserIdAsync(Guid UserId);
    }
}
