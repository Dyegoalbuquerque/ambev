using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Item of Sale in the system with market sales online.
/// </summary>
public class SaleItem : BaseEntity
{
    
    public string ProductId { get; set; }

        
    public string ProductName { get; set; }
    
    
    public Sale Sale{ get; set; }
    
    public Guid SaleId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal TotalAmount => (Quantity * UnitPrice);

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}
