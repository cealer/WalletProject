using FluentValidation;
using Microsoft.Extensions.Logging;
using WalletService.API.Application.Commands.WalletCommands;

namespace WalletService.API.Application.Validations
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator(ILogger<CreateWalletCommand> logger)
        {
            RuleFor(command => command.UserId).NotEmpty();
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
