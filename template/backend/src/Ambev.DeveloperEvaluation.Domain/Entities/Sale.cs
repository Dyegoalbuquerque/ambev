using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Sale in the system with market sales online.
/// </summary>
public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = Guid.NewGuid().ToString();

    public DateTime SaleAt { get; set; }

    public string CustomerId { get; set; }

    public string CustomerName { get; set; }

    public decimal TotalSaleAmount { get; set; }

    public string BranchSale { get; set; }

    public decimal TotalDiscounts { get; set; }

    public bool IsCancelled { get; set; } = false;

    public List<SaleItem> Products { get; set; } = new List<SaleItem>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int GetNumberIdenticalsItens()
    {
        return Products.GroupBy(item => item.ProductId).Select(group => group.Sum(item => item.Quantity))
                       .OrderByDescending(totalQuantity => totalQuantity)
                       .FirstOrDefault();
    }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}