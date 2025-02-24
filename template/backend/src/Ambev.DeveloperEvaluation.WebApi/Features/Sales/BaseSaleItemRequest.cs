
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class BaseSaleItemRequest
{        
    public string ProductId { get; set; }
        
    public string ProductName { get; set; }    
    
    public int Quantity { get; set; }    
    
    public decimal UnitPrice { get; set; }
}