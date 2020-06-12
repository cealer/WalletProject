using FluentValidation;
using Microsoft.Extensions.Logging;
using WalletService.API.Application.Commands.PaymentCommands;

namespace WalletService.API.Application.Validations
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator(ILogger<CreatePaymentCommand> logger)
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.Amount).GreaterThan(0);
            RuleFor(command => command.Amount).LessThan(999999.99m);
            RuleFor(command => command.PaymentMethodId).NotNull();
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
