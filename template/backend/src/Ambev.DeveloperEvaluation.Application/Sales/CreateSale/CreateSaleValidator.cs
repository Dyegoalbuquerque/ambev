using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;


public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {        
        RuleFor(sale => sale.BranchSale).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.CustomerId).NotEmpty().Length(1, 50);
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 50);        
        RuleFor(sale => sale.SaleAt).Must(date => date != default(DateTime));  
        RuleFor(sale => sale.Products).NotEmpty();  
    }
}