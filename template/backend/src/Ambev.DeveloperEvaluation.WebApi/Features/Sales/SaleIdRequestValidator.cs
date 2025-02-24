using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class SaleIdRequestValidator : AbstractValidator<SaleIdRequest>
{
  
    public SaleIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
