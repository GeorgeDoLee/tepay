using FluentValidation;
using TePay.Models.Requests.Details;

namespace TePay.Validators.DetailValidators;

/// <summary>
/// Validator for the <see cref="Amount"/> model.
/// </summary>
public class AmountValidator : AbstractValidator<Amount>
{
    public AmountValidator()
    {
        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("Currency is required.")
            .Must(currency => currency == "GEL" || currency == "USD" || currency == "EUR")
            .WithMessage("Currency must be one of the following: GEL, USD, EUR.");

        RuleFor(x => x.Total)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total must be a non-negative number.");

        RuleFor(x => x.SubTotal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("SubTotal must be a non-negative number.");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax must be a non-negative number.");

        RuleFor(x => x.Shipping)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Shipping must be a non-negative number.");
    }
}
