namespace Accounts.Api.Validators.AccountValidators
{
    using FluentValidation;
    using Services.AccountService.Requests;

    public class CreateValidator : AbstractValidator<Create>
    {
        public CreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.DisplayName)
                .NotEmpty();

            RuleFor(c => c.IdentificationNumber)
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty();
        }
    }
}