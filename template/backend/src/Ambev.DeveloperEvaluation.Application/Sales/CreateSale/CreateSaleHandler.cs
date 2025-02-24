using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
{    
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisherSale _eventPublisher;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ILogger<CreateSaleHandler> logger, ISaleRepository SaleRepository, IMapper mapper, IEventPublisherSale eventPublisher)
    {
        _logger = logger;
        _saleRepository = SaleRepository;
        _eventPublisher = eventPublisher;
        _mapper = mapper;
    }

    public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);       

        int numberItensIdenticals = SaleCalculation.CalculateNumbersIdenticalsItens(sale);

        if (SaleCalculation.ShouldNotSaleMoreThan20Identicals(numberItensIdenticals))
        {
            _logger.LogWarning($"Sale not should have more than 20 identicals itens"); 
            throw new InvalidOperationException($"Sale not should have more than 20 identicals itens");
        }
        sale.SaleNumber = Guid.NewGuid().ToString();      
        var totalAmountProducts = SaleCalculation.CalculateTotalAmount(sale);  
        sale.TotalDiscounts = SaleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = SaleCalculation.CalculateTotalSaleAmount(totalAmountProducts, sale.TotalDiscounts);

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<SaleResult>(createdSale);

        _eventPublisher.PublishEvent(EventPublisherEnum.SaleCreated);

        return result;
    }
}
