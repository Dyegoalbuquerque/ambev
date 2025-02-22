using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.Id).NotEmpty();
        RuleFor(sale => sale.BranchSale).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.CustomerId).NotEmpty().Length(1, 50);
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.SaleAt).Must(date => date != default(DateTime));
        RuleFor(sale => sale.Products).NotEmpty();
        RuleFor(sale => sale.SaleNumber).NotEmpty();
    }
}