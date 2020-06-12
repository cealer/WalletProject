using System;

namespace WalletService.API.Application.Queries.PaymentQueries
{
    public class PaymentViewModels
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
    }
}
