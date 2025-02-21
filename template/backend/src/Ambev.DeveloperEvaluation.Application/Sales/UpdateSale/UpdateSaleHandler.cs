using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;

    private readonly IEventPublisherSale _eventPublisher;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, IEventPublisherSale eventPublisher, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _eventPublisher = eventPublisher;
        _mapper = mapper;
    }

    public async Task<SaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var saleSent = _mapper.Map<Sale>(command);
        
        var sale = await _saleRepository.GetByIdAsync(saleSent.Id);

        if (sale.IsCancelled)
            throw new InvalidOperationException($"Sale not should have updated by it's canceled");

        var saleCalculation = new SaleCalculation(); 
        int numberItensIdenticals = sale.GetNumberIdenticalsItens();

        if (saleCalculation.ShouldNotSaleMoreThan20Identicals(numberItensIdenticals))
            throw new InvalidOperationException($"Sale not should have more than 20 identicals itens");

        sale.TotalDiscounts = saleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = saleCalculation.CalculateTotalSaleAmount(numberItensIdenticals, sale.TotalDiscounts);

        var saleItens = await _saleItemRepository.GetBySaleIdAsync(sale.Id);

        foreach (var item in saleItens)
        {
            await _saleItemRepository.DeleteAsync(item.Id, cancellationToken);
            _eventPublisher.PublishEvent(EventPublisherEnum.ItemCancelled);
        }

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        var result = _mapper.Map<SaleResult>(updatedSale);

        _eventPublisher.PublishEvent(EventPublisherEnum.SaleModified);

        return result;
    }
}
