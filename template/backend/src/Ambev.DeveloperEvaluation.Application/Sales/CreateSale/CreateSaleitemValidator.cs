using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{    
    public CreateSaleItemCommandValidator()
    {        
        RuleFor(saleItem => saleItem.ProductId).NotEmpty().Length(1, 50);
        RuleFor(saleItem => saleItem.ProductName).NotEmpty().Length(3, 50);
        RuleFor(saleItem => saleItem.Quantity).GreaterThan(0);        
        RuleFor(saleItem => saleItem.UnitPrice).GreaterThan(0);       
    }
}