namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class BaseSaleRequest
{            
    public DateTime SaleAt { get; set; }
    
    public string CustomerId { get; set; }
    
    public string CustomerName { get; set; }
    
    public string BranchSale { get; set; }
    
    public bool IsCancelled { get; set; }    
    
    public List<BaseSaleItemRequest> Products { get; set; }
}