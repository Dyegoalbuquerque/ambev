﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly ILogger<SalesController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(ILogger<SalesController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<BaseSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSales([FromBody] BaseSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new SaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        
        _logger.LogInformation("Sale created successfully");

        return Created(string.Empty, new ApiResponseWithData<BaseSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<BaseSaleResponse>(response)
        });
    }


    /// <summary>
    /// Updates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<BaseSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSales([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var validator = new SaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        
        var response = await _mediator.Send(command, cancellationToken);
        
        _logger.LogInformation("Sale updated successfully");

        return Created(string.Empty, new ApiResponseWithData<BaseSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = _mapper.Map<BaseSaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<BaseSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSales([FromRoute] Guid id, CancellationToken cancellationToken)
    {        
        var request = new SaleIdRequest { Id = id };
        var validator = new SaleIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Sale retrieved successfully");

        return Ok(new ApiResponseWithData<BaseSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<BaseSaleResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the sale was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSales([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new SaleIdRequest { Id = id };
        var validator = new SaleIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Sale deleted successfully");

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }
}
