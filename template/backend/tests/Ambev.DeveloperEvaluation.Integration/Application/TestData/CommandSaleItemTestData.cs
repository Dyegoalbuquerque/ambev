using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class CommandSaleItemTestData : Faker<CreateSaleItemCommand>
{
    public CommandSaleItemTestData()
    {
        RuleFor(p => p.ProductId, f => f.Random.Int(1, 10).ToString());
        RuleFor(p => p.ProductName, f => f.Commerce.ProductName());
        RuleFor(p => p.Quantity, f => f.Random.Int(1, 10));
        RuleFor(p => p.UnitPrice, f => f.Finance.Amount(5, 100));
    }
}
