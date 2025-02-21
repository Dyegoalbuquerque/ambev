using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;


public class UpdateSaleCommand : IRequest<SaleResult>
{
    public Guid Id {get; set;}
    
    public string SaleNumber { get; set; }

    
    public DateTime SaleAt { get; set; }

    
    public string CustomerId { get; set; }

    
    public string CustomerName { get; set; }

    
    public decimal TotalSaleAmount { get; set; }

    
    public string BranchSale { get; set; }

    
    public decimal TotalDiscounts { get; set; }

    
    public bool IsCancelled { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

      
    public List<UpdateSaleItemCommand> Products { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}