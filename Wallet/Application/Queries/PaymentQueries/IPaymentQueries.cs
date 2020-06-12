using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WalletService.API.Application.Queries.PaymentQueries
{
    public interface IPaymentQueries
    {
        Task<List<PaymentViewModels>> GetPaymentsByUserIdAsync(Guid userId);
    }
}
