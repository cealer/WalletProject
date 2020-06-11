using System;
using System.Collections.Generic;
using System.Linq;
using WalletService.Service.Domain.Exceptions;
using WalletService.Service.Domain.SeedWork;

namespace WalletService.Service.Domain.AggregatesModel.PaymentAggregate
{
    public class PaymentMethod : Enumeration
    {
        public static PaymentMethod Cash = new PaymentMethod(1, nameof(Cash).ToLowerInvariant());
        public static PaymentMethod BitCoin = new PaymentMethod(2, nameof(BitCoin).ToLowerInvariant());
        public static PaymentMethod GooglePay = new PaymentMethod(3, nameof(GooglePay).ToLowerInvariant());

        public PaymentMethod(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<PaymentMethod> List() =>
            new[] { Cash, BitCoin, GooglePay };

        public static PaymentMethod FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new WalletDomainException($"Possible values for TypePayment: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static PaymentMethod From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new WalletDomainException($"Possible values for TypePayment: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
