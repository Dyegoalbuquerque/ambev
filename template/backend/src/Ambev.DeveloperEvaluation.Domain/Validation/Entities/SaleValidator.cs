using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Entities;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.BranchSale)
            .NotEmpty()
            .MinimumLength(3).WithMessage("BranchSale must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("BranchSale cannot be longer than 50 characters.");
            
        RuleFor(sale => sale.CustomerName)
            .NotEmpty()
            .MinimumLength(3).WithMessage("CustomerName must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("CustomerName cannot be longer than 50 characters.");        
            
        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .MinimumLength(1).WithMessage("CustomerId must be at least 1 characters long.")
            .MaximumLength(50).WithMessage("CustomerId cannot be longer than 50 characters.");
        
        RuleFor(sale => sale.SaleAt).Must(date => date != default(DateTime));
        
        RuleFor(sale => sale.Products).NotEmpty();

        RuleFor(sale => sale.TotalSaleAmount).GreaterThan(0);
        
        RuleFor(sale => sale.CreatedAt).Must(date => date != default(DateTime));
    }
}
