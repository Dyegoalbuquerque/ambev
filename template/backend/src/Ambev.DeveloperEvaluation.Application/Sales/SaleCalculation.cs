
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Calculation of Sale in the system with market sales online.
/// </summary>
public static class SaleCalculation
{    
    public static int CalculateNumbersIdenticalsItens(Sale sale)
    {
        return sale.Products.GroupBy(item => item.ProductId).Select(group => group.Sum(item => item.Quantity))
                       .OrderByDescending(totalQuantity => totalQuantity)
                       .FirstOrDefault();
    }
    public static decimal CalculateDiscount(int numberItensIdenticals)
    {        
        if (numberItensIdenticals >= 10 && numberItensIdenticals <= 20) return 20m;
        if (numberItensIdenticals > 4 && numberItensIdenticals < 10) return 10m;
        return 0m;        
    }

     public static decimal CalculateTotalAmount(Sale sale)
    {        
        return sale.Products.Sum(si => si.TotalAmount);        
    }

    public static decimal CalculateTotalSaleAmount(decimal totalAmountProducts, decimal totalDiscounts)
    {
        return totalAmountProducts - (totalAmountProducts * (totalDiscounts / 100));
    }

    public static bool ShouldNotSaleMoreThan20Identicals(int numberItensIdenticals) => numberItensIdenticals > 20;
}