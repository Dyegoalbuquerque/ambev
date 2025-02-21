using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleItemTestData : Faker<SaleItem>
{
    public SaleItemTestData()
    {
        RuleFor(p => p.ProductId, f => f.Random.Int(1, 10).ToString());
        RuleFor(p => p.ProductName, f => f.Commerce.ProductName());
        RuleFor(p => p.Quantity, f => f.Random.Int(1, 10));
        RuleFor(p => p.UnitPrice, f => f.Finance.Amount(5, 100));
        RuleFor(p => p.TotalAmount, (f, p) => (p.UnitPrice * p.Quantity));
    }
}
