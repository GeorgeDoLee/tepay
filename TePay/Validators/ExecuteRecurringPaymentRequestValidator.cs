using FluentValidation;
using TePay.Models.Requests;
using TePay.Validators.DetailValidators;

namespace TePay.Validators;

/// <summary>
/// Validator for the <see cref="ExecuteRecurringPaymentRequest"/> class
/// </summary>
public class ExecuteRecurringPaymentRequestValidator : AbstractValidator<ExecuteRecurringPaymentRequest>
{
    public ExecuteRecurringPaymentRequestValidator()
    {
        RuleFor(x => x.RecID)
            .NotEmpty()
            .WithMessage("RecID is required.");

        RuleFor(x => x.Extra)
            .MaximumLength(25)
            .WithMessage("Extra must not exceed 25 characters.")
            .Matches("^[\x00-\x7F]*$")
            .WithMessage("Extra must contain only non-Unicode (ANSI) symbols.");

        RuleFor(x => x.Extra2)
            .MaximumLength(52)
            .WithMessage("Extra2 must not exceed 52 characters.")
            .Matches("^[\x00-\x7F]*$")
            .WithMessage("Extra2 must contain only non-Unicode (ANSI) symbols.");

        RuleFor(x => x.Initiator)
            .Must(value => string.IsNullOrEmpty(value) || value == "merchant")
            .WithMessage("Initiator must be 'merchant' or left empty.");

        RuleFor(x => x.Money)
            .NotNull()
            .WithMessage("Money is required")
            .SetValidator(new MoneyValidator());
    }
}
