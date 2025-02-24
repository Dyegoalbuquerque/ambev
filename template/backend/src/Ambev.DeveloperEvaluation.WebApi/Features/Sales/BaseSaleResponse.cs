
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class BaseSaleResponse
{
    public Guid Id {get; set;}
    
    public string SaleNumber { get; set; }

    
    public DateTime SaleAt { get; set; }

    
    public string CustomerId { get; set; }

    
    public string CustomerName { get; set; }

    
    public decimal TotalSaleAmount { get; set; }

    
    public string BranchSale { get; set; }

    
    public decimal TotalDiscounts { get; set; }

    
    public List<BaseSaleItemResponse> Products {get; set;}

}
