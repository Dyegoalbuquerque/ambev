using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisherSale _eventPublisher;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository SaleRepository, IMapper mapper, IEventPublisherSale eventPublisher)
    {
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

        var saleCalculation = new SaleCalculation(); 
        int numberItensIdenticals = sale.GetNumberIdenticalsItens();

        if (saleCalculation.ShouldNotSaleMoreThan20Identicals(numberItensIdenticals))
            throw new InvalidOperationException($"Sale not should have more than 20 identicals itens");

        sale.TotalDiscounts = saleCalculation.CalculateDiscount(numberItensIdenticals);
        sale.TotalSaleAmount = saleCalculation.CalculateTotalSaleAmount(numberItensIdenticals, sale.TotalDiscounts);

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<SaleResult>(createdSale);

        _eventPublisher.PublishEvent(EventPublisherEnum.SaleCreated);

        return result;
    }
}
