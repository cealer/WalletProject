using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Domain.AggregatesModel.WalletAggregate;
using WalletService.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Service.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public WalletRepository(WalletContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Wallet Add(Wallet wallet)
        {
            if (wallet.IsTransient())
            {
                return _context.Wallets
                    .Add(wallet)
                    .Entity;
            }
            else
            {
                return wallet;
            }
        }

        public Wallet Update(Wallet wallet)
        {
            return _context.Wallets
                    .Update(wallet)
                    .Entity;
        }

        public async Task<Wallet> GetAsync(Guid WalletId)
        {
            var wallet = await _context.Wallets
                   .Where(b => b.Id == WalletId)
                   .FirstOrDefaultAsync();

            return wallet;
        }

        public async Task<Wallet> GetByUserIdAsync(Guid UserId)
        {
            var wallet = await _context.Wallets
                   .Where(b => b.UserId == UserId)
                   .FirstOrDefaultAsync();
            return wallet;
        }

    }
}
