using Accounts.Api.Services.AccountService.Requests;
using FluentValidation;

namespace Accounts.Api.Validators.AccountValidators
{
    public class CreateValidator : AbstractValidator<Create>
    {
        public CreateValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.IdentificationNumber)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty();
        }
    }
}