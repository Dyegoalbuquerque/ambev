using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleTestData : Faker<Sale>
{
    public SaleTestData(int countItens = 0)
    {
        RuleFor(s => s.SaleNumber, f => f.Random.Int(100000, 999999).ToString());
        RuleFor(s => s.CustomerId, f => f.Random.Int(1, 1000).ToString());
        RuleFor(s => s.SaleAt, f => f.Date.Past(1));
        RuleFor(s => s.CustomerName, f => f.Name.FullName());
        RuleFor(s => s.TotalSaleAmount, f => f.Finance.Amount(50, 1000));
        RuleFor(s => s.BranchSale, f => f.Address.City());
        RuleFor(s => s.IsCancelled, f => f.Random.Bool());
        RuleFor(s => s.TotalDiscounts, f => 0);
        RuleFor(s => s.Products, f =>  Enumerable.Range(0, countItens).Select(_ => new SaleItemTestData().Generate()).ToList());
    }
}
