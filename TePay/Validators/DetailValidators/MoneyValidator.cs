using FluentValidation;
using TePay.Models.Requests.Details;

namespace TePay.Validators.DetailValidators;

/// <summary>
/// Validator for the <see cref="Money"/> class
/// </summary>
public class MoneyValidator : AbstractValidator<Money>
{
    public MoneyValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage("Amount is required.")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("Currency is required.")
            .Must(currency => currency == "GEL" || currency == "USD" || currency == "EUR")
            .WithMessage("Currency must be one of the following: GEL, USD, EUR.");
    }
}
