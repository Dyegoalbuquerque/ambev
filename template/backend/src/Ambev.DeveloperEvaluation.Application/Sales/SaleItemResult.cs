namespace Ambev.DeveloperEvaluation.Application.Sales;


public class SaleItemResult
{
    public Guid Id { get; set; }
    
    public string ProductId { get; set; }
        
    public string ProductName { get; set; }
    
    public Guid SaleId { get; set; }
    
    public int Quantity { get; set; }    
    
    public decimal UnitPrice { get; set; }
    
    public decimal TotalAmount => (Quantity * UnitPrice);
}
