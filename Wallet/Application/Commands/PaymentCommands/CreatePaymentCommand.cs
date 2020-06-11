using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.Service.Domain.AggregatesModel.PaymentAggregate;

namespace WalletService.API.Application.Commands.PaymentCommands
{
    public class CreatePaymentCommand : IRequest<bool>
    {
        public Guid UserId { get; }
        public decimal Amount { get; }
        public int PaymentMethodId { get; }

        public CreatePaymentCommand(Guid userId, decimal amount, int paymentMethodId)
        {
            UserId = userId;
            Amount = amount;
            PaymentMethodId = paymentMethodId;
        }
    }
}
