using Erp.Trucks.DataTransfer;
using FluentValidation;

namespace Erp.Trucks.Validators;

public class CreateTruckDtoValidator : AbstractValidator<CreateTruckDto>
{
    public CreateTruckDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}