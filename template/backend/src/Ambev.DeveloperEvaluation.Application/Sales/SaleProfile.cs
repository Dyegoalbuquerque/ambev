using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<Sale, SaleResult>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));
        CreateMap<CreateSaleItemCommand, SaleItem>();       
        CreateMap<Sale, SaleResult>();
        CreateMap<UpdateSaleCommand, Sale>();        
        CreateMap<UpdateSaleItemCommand, SaleItem>();
        CreateMap<SaleItem, SaleItemResult>();
    }
}
