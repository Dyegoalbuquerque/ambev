using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<BaseSaleRequest, CreateSaleCommand>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));
        CreateMap<SaleResult, BaseSaleResponse>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));        
        CreateMap<BaseSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<SaleItemResult, BaseSaleItemResponse>();
        
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));
        CreateMap<SaleResult, BaseSaleResponse>().ForMember(d => d.Products, o => o.MapFrom(s => s.Products));
        CreateMap<BaseSaleItemRequest, UpdateSaleItemCommand>();
        CreateMap<SaleItemResult, BaseSaleItemResponse>();

        CreateMap<Guid, GetSaleCommand>().ConstructUsing(id => new GetSaleCommand(id));
        CreateMap<BaseSaleRequest, GetSaleCommand>();
        CreateMap<SaleResult, BaseSaleResponse>();
        CreateMap<SaleItemResult, BaseSaleItemResponse>();
        
        CreateMap<Guid, Application.Sales.DeleteSale.DeleteSaleCommand>().ConstructUsing(id => new Application.Sales.DeleteSale.DeleteSaleCommand(id));
    }
}
