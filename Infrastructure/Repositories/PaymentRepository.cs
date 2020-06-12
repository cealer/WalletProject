using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WalletService.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Service.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly WalletContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PaymentRepository(WalletContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Payment Add(Payment payment)
        {
            if (payment.IsTransient())
            {
                return _context.Payments
                    .Add(payment)
                    .Entity;
            }
            else
            {
                return payment;
            }
        }

        public Payment Update(Payment Payment)
        {
            return _context.Payments
                    .Update(Payment)
                    .Entity;
        }

        public async Task<Payment> GetAsync(Guid PaymentId)
        {
            var payment = await _context.Payments
                   .Where(b => b.Id == PaymentId)
                   .FirstOrDefaultAsync();

            return payment;
        }

        public async Task<Payment> GetByWalletIdAsync(Guid WalletId)
        {
            var payment = await _context.Payments
                   .Where(b => b.GetWalletId == WalletId)
                   .FirstOrDefaultAsync();
            return payment;
        }

    }
}
