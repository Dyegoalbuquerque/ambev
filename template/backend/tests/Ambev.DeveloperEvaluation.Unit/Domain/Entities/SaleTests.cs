using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that when a Purchases above 4 identical items have a 10% discount
    /// </summary>
    [Fact(DisplayName = "Sale above 4 identicals should change to 10% discount")]
    public void Given_Sale_with4IdenticalsItems_When_ApplyDiscount_Then_ShouldHave10Percent()
    {
        var sale = new SaleTestData(5).Generate();
        sale.Products = sale.Products.Select(item => new SaleItem
        {
            ProductId = "11",
            ProductName = item.ProductName,
            Quantity = 1,
            UnitPrice = item.UnitPrice
        }).ToList();

        var saleCalculation = new SaleCalculationTestData().Generate();

        var numberItensIdenticals = sale.GetNumberIdenticalsItens();
        sale.TotalDiscounts = saleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = saleCalculation.CalculateTotalSaleAmount(numberItensIdenticals, sale.TotalDiscounts);

        Assert.Equal(10m, sale.TotalDiscounts);
    }

    /// <summary>
    /// Tests that when a Purchases between 10 and 20 identical items have a 20% discount
    /// </summary>
    [Fact(DisplayName = "Sale between 10 and 20 identical items have a 20% discount")]
    public void Given_Sale_Between10and20Identical_When_ApplyDiscount_Then_ShouldHave20Percent()
    {
        var sale = new SaleTestData(12).Generate();
        sale.Products = sale.Products.Select(item => new SaleItem
        {
            ProductId = "47",
            ProductName = item.ProductName,
            Quantity = 1,
            UnitPrice = item.UnitPrice
        }).ToList();

        var saleCalculation = new SaleCalculationTestData().Generate();

        var numberItensIdenticals = sale.GetNumberIdenticalsItens();
        sale.TotalDiscounts = saleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = saleCalculation.CalculateTotalSaleAmount(numberItensIdenticals, sale.TotalDiscounts);

        Assert.Equal(20m, sale.TotalDiscounts);
    }

    /// <summary>
    /// Tests that when a Purchases below 4 items cannot have a discount
    /// </summary>
    [Fact(DisplayName = "Sale below 4 items cannot have a discount")]
    public void Given_Sale_Below4items_When_ApplyDiscount_Then_CannotHaveDiscount()
    {
        var sale = new SaleTestData(3).Generate();
        sale.Products = sale.Products.Select(item => new SaleItem
        {
            ProductId = "7",
            ProductName = item.ProductName,
            Quantity = 1,
            UnitPrice = item.UnitPrice
        }).ToList();

        var saleCalculation = new SaleCalculationTestData().Generate();

        var numberItensIdenticals = sale.GetNumberIdenticalsItens();
        sale.TotalDiscounts = saleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = saleCalculation.CalculateTotalSaleAmount(numberItensIdenticals, sale.TotalDiscounts);

        Assert.Equal(0m, sale.TotalDiscounts);
    }
}
