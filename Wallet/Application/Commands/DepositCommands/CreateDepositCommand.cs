using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.API.Application.Commands.DepositCommands
{
    public class CreateDepositCommand : IRequest<bool>
    {
        public Guid UserId { get; }
        public decimal Amount { get; }

        public CreateDepositCommand(Guid userId, decimal amount)
        {
            UserId = userId;
            Amount = amount;
        }

    }
}
