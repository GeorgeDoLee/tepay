using FluentValidation;
using TePay.Models.Requests;
using TePay.Models.Requests.Details;
using TePay.Validators.DetailValidators;

namespace TePay.Validators;

/// <summary>
/// Validator for the <see cref="CreatePaymentRequest"/> model.
/// </summary>
public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage("Amount is required.")
            .SetValidator(new AmountValidator());

        RuleFor(x => x.ReturnUrl)
            .NotEmpty()
            .WithMessage("ReturnUrl is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("ReturnUrl must be a valid URL.");

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

        RuleFor(x => x.UserIpAddress)
            .MaximumLength(15)
            .WithMessage("UserIpAddress must not exceed 15 characters.")
            .Matches(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$")
            .WithMessage("UserIpAddress must be a valid IP address.");

        RuleFor(x => x.ExpirationMinutes)
            .GreaterThan(0)
            .WithMessage("ExpirationMinutes must be greater than 0.");

        RuleForEach(x => x.Methods)
            .Must(m => Enum.IsDefined(typeof(PaymentMethod), m))
            .WithMessage("Each item in Methods must be a valid PaymentMethod enum value.");

        RuleForEach(x => x.InstallmentProducts)
            .SetValidator(new InstallmentProductValidator());

        RuleFor(x => x.CallbackUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("ReturnUrl must be a valid URL.");

        RuleFor(x => x.Language)
            .Must(lang => string.IsNullOrEmpty(lang) || lang == "KA" || lang == "EN")
            .WithMessage("Language must be 'KA' or 'EN'.");

        RuleFor(x => x.SaveCardToDate)
            .Matches(@"^(0[1-9]|1[0-2])\d{2}$")
            .WithMessage("SaveCardToDate must be in MMYY format.");

        RuleFor(x => x.Description)
            .MaximumLength(30)
            .WithMessage("Description must not exceed 30 characters.");
    }
}
