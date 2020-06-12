using MediatR;
using System;

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
