using Accounts.Api.Services.AccountService.Requests;
using FluentValidation;

namespace Accounts.Api.Validators.AccountValidators
{
    public class UpdateValidator : AbstractValidator<Update>
    {
        public UpdateValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();

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