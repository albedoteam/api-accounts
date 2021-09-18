namespace Accounts.Api.Validators.AccountValidators
{
    using System.Text.RegularExpressions;
    using FluentValidation;
    using Services.AccountService.Requests;

    public class UpdateValidator : AbstractValidator<Update>
    {
        public UpdateValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .Matches("^[0-9a-fA-F]{24}$", RegexOptions.IgnoreCase);

            RuleFor(c => c.DisplayName)
                .NotEmpty();

            RuleFor(c => c.IdentificationNumber)
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty();
        }
    }
}