using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Entities;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(sale => sale.ProductId)
         .NotEmpty()
         .MinimumLength(1).WithMessage("ProductId must be at least 1 characters long.")
         .MaximumLength(50).WithMessage("ProductId cannot be longer than 50 characters.");

        RuleFor(sale => sale.ProductName)
         .NotEmpty()
         .MinimumLength(3).WithMessage("ProductName must be at least 3 characters long.")
         .MaximumLength(50).WithMessage("ProductName cannot be longer than 50 characters.");

        RuleFor(sale => sale.Quantity).GreaterThan(0);

        RuleFor(sale => sale.UnitPrice).GreaterThan(0);
    }
}
