using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.ORM;

public class CreateSaleHandlerTests : IClassFixture<InMemoryDbFixture>
{
    public DefaultContext _context { get; private set; }
    private readonly CreateSaleHandler _handler;
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisherSale _eventPublisher;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandlerTests(InMemoryDbFixture fixture)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateSaleCommand, Sale>();
            cfg.CreateMap<Sale, SaleResult>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));
            cfg.CreateMap<CreateSaleItemCommand, SaleItem>();
            cfg.CreateMap<Sale, SaleResult>();
            cfg.CreateMap<SaleItem, SaleItemResult>();
        });
        _mapper = new Mapper(configuration);
        _context = fixture.Context;
        _logger = fixture.ServiceProvider.GetRequiredService<ILogger<CreateSaleHandler>>();
        _mapper = fixture.ServiceProvider.GetRequiredService<IMapper>();
        _eventPublisher = fixture.ServiceProvider.GetRequiredService<IEventPublisherSale>();
        _saleRepository = new SaleRepository(_context);
        _handler = new CreateSaleHandler(_logger, _saleRepository, _mapper, _eventPublisher);
    }

    [Fact]
    public async Task CreateSaleHandler_ShouldCreateSaleSuccessfully()
    {
        // Arrange
        var command = new CommandSaleTestData(3).Generate();

        var sale = new Sale()
        {
            BranchSale = command.BranchSale,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            SaleAt = command.SaleAt,
            SaleNumber = command.SaleNumber,
            TotalDiscounts = command.TotalDiscounts,
            TotalSaleAmount = command.TotalSaleAmount,
            Products = command.Products.Select(item => new SaleItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };

        var totalAmount = SaleCalculation.CalculateTotalAmount(sale);
        var numberItensIdenticals = SaleCalculation.CalculateNumbersIdenticalsItens(sale);
        sale.TotalDiscounts = SaleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = SaleCalculation.CalculateTotalSaleAmount(totalAmount, sale.TotalDiscounts);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sale.TotalSaleAmount, result.TotalSaleAmount);
    }
}
