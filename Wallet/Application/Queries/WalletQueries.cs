using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.API.Application.Queries
{
    public class WalletQueries : IWalletQueries
    {
        private string _connectionString = string.Empty;

        public WalletQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<WalletViewModel> GetWalletByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<WalletViewModel>(
                   @"select Balance
                     from [wallet_service].[Wallets]
                     WHERE UserId=@userId"
                        , new { userId }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result.FirstOrDefault();
            }
        }

        public async Task<WalletViewModel> GetDepositsAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<WalletViewModel>(
                   @"select Amount,Date
                     from deposits
                     WHERE o.WalletId=@id"
                        , new { id }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result.FirstOrDefault();
            }
        }

    }
}
