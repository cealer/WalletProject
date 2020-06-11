using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.DepositCommands;

namespace WalletService.API.Application.Validations
{
    public class CreateDepositCommandValidator : AbstractValidator<CreateDepositCommand>
    {
        public CreateDepositCommandValidator(ILogger<CreateDepositCommand> logger)
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.Amount).GreaterThan(0).WithMessage("Amount can be greater than 0");
            RuleFor(command => command.Amount).LessThan(999999.99m).WithMessage("Amount can be less than 999999.99");
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

    }
}
