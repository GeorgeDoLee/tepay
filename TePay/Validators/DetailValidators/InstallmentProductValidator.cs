using FluentValidation;
using TePay.Models.Requests.Details;

namespace TePay.Validators.DetailValidators;

/// <summary>
/// Validator for the <see cref="InstallmentProduct"/> model.
/// </summary>
public class InstallmentProductValidator : AbstractValidator<InstallmentProduct>
{
    public InstallmentProductValidator()
    {
        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("Price is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative number.");

        RuleFor(x => x.Quantity)
            .NotNull()
            .WithMessage("Quantity is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity must be a non-negative number.");
    }
}
