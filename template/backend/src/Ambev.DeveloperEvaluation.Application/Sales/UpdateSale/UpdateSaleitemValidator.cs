using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;

/// <summary>
/// Validator for UpdateSaleItemCommand that defines validation rules for SaleItem update command.
/// </summary>
public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{

    public UpdateSaleItemCommandValidator()
    {        
        RuleFor(saleItem => saleItem.ProductId).NotEmpty().Length(1, 50);
        RuleFor(saleItem => saleItem.ProductName).NotEmpty().Length(3, 50);
        RuleFor(saleItem => saleItem.Quantity).GreaterThan(0);        
        RuleFor(saleItem => saleItem.UnitPrice).GreaterThan(0);       
    }
}