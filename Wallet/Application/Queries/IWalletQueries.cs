using System;
using System.Threading.Tasks;

namespace WalletService.API.Application.Queries
{
    public interface IWalletQueries
    {
        Task<WalletViewModel> GetWalletByUserIdAsync(Guid userId);
        Task<WalletViewModel> GetDepositsAsync(Guid id);
    }
}
