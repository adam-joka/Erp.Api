using Erp.Trucks.DataTransfer;
using FluentValidation;

namespace Erp.Trucks.Validators;

public class UpdateTruckDtoValidator : AbstractValidator<UpdateTruckDto>
{
    public UpdateTruckDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}