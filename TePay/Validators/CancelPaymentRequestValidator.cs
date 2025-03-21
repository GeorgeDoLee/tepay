using FluentValidation;
using TePay.Models.Requests;

namespace TePay.Validators;

/// <summary>
/// Validator for the <see cref="CancelPaymentRequest"/> model.
/// </summary>
public class CancelPaymentRequestValidator : AbstractValidator<CancelPaymentRequest>
{
    public CancelPaymentRequestValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage("Amount must not be null.")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Extra)
            .Matches(@"^([A-Z]{2}[0-9]{2}[A-Z0-9]{4,30})$")
            .WithMessage("Extra field must be a valid IBAN format.");
    }
}
