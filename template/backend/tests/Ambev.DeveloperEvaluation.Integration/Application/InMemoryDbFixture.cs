using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Sales;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.ORM.Repositories;

public class InMemoryDbFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; }
    public DefaultContext Context { get; private set; }

    public InMemoryDbFixture()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<DefaultContext>(options => options.UseInMemoryDatabase("TestDatabase"));
       
        serviceCollection.AddLogging();
        serviceCollection.AddScoped<ISaleRepository, SaleRepository>();
        serviceCollection.AddScoped<IEventPublisherSale, EventPublisherSale>();
        serviceCollection.AddScoped<IMapper>(provider => new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateSaleCommand, Sale>();
            cfg.CreateMap<Sale, SaleResult>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));
            cfg.CreateMap<CreateSaleItemCommand, SaleItem>();
            cfg.CreateMap<Sale, SaleResult>();
            cfg.CreateMap<SaleItem, SaleItemResult>();
        })));

        serviceCollection.AddScoped<CreateSaleHandler>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
        Context = ServiceProvider.GetRequiredService<DefaultContext>();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
