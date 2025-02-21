
namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Calculation of Sale in the system with market sales online.
/// </summary>
public class SaleCalculation
{    
    public decimal CalculateDiscount(int numberItensIdenticals)
    {        
        if (numberItensIdenticals >= 10 && numberItensIdenticals <= 20) return 20m;
        if (numberItensIdenticals > 4 && numberItensIdenticals < 10) return 10m;
        return 0m;        
    }

    public decimal CalculateTotalSaleAmount(decimal totalAmountProducts, decimal totalDiscounts)
    {
        return totalAmountProducts - (totalAmountProducts * (totalDiscounts / 100));
    }

    public bool ShouldNotSaleMoreThan20Identicals(int numberItensIdenticals) => numberItensIdenticals > 20;
}