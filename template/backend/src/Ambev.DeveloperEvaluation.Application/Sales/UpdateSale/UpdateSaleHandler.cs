using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResult>
{
    private readonly ILogger<UpdateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;

    private readonly IEventPublisherSale _eventPublisher;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ILogger<UpdateSaleHandler> logger, ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, IEventPublisherSale eventPublisher, IMapper mapper)
    {
        _logger = logger;
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

        var isCancelled = await _saleRepository.CheckIsCanceledAsync(saleSent.Id);

        if (isCancelled)
        {
            _logger.LogWarning($"Sale not should have updated by it's canceled");
            throw new InvalidOperationException($"Sale not should have updated by it's canceled");
        }

        int numberItensIdenticals = SaleCalculation.CalculateNumbersIdenticalsItens(saleSent);

        if (SaleCalculation.ShouldNotSaleMoreThan20Identicals(numberItensIdenticals))
        {
            _logger.LogWarning($"Sale not should have more than 20 identicals itens");
            throw new InvalidOperationException($"Sale not should have more than 20 identicals itens");
        }

        var totalAmountProducts = SaleCalculation.CalculateTotalAmount(saleSent);  
        saleSent.TotalDiscounts = SaleCalculation.CalculateDiscount(numberItensIdenticals);
        saleSent.TotalSaleAmount = SaleCalculation.CalculateTotalSaleAmount(totalAmountProducts, saleSent.TotalDiscounts);

        var saleItens = await _saleItemRepository.GetBySaleIdAsync(saleSent.Id);

        foreach (var item in saleItens)
        {
            await _saleItemRepository.DeleteAsync(item.Id, cancellationToken);
            _eventPublisher.PublishEvent(EventPublisherEnum.ItemCancelled);
        }

        var updatedSale = await _saleRepository.UpdateAsync(saleSent, cancellationToken);
        var result = _mapper.Map<SaleResult>(updatedSale);

        _eventPublisher.PublishEvent(EventPublisherEnum.SaleModified);

        return result;
    }
}
