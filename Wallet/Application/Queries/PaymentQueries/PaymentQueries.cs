using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.API.Application.Queries.PaymentQueries
{
    public class PaymentQueries : IPaymentQueries
    {
        private readonly string _connectionString = string.Empty;

        public PaymentQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<List<PaymentViewModels>> GetPaymentsByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<PaymentViewModels>(
                   @"select p.Amount,p.Date,pm.Name
                     from [wallet_service].[Payments] p
                     INNER JOIN [wallet_service].[PaymentsMethods] pm
                     on p.PaymenthMethodId=pm.PaymenthMethodId
                     WHERE p.UserId=@userId"
                        , new { userId }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result.ToList();
            }
        }
    }
}
