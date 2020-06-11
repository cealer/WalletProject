using MediatR;
using System;
using System.Runtime.Serialization;

namespace WalletService.API.Application.Commands.WalletCommands
{
    public class CreateWalletCommand : IRequest<bool>
    {
        [DataMember]
        public Guid UserId { get; private set; }

        public CreateWalletCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
