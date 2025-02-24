using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemCommand : IRequest<SaleItemResult>
{
    
    public string ProductId { get; set; }

        
    public string ProductName { get; set; }
    
    
    public int Quantity { get; set; }
    
    
    public decimal UnitPrice { get; set; }


    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}