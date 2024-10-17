using FluentValidation;

namespace BCA.CAMS.Application.Features.AuctionManagement.Models.Validators;

public class VehicleValidator : AbstractValidator<Vehicle>
{
    public VehicleValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.Manufacturer).NotNull();
        RuleFor(v => v.CarModel).NotNull();
        RuleFor(v => v.Year).InclusiveBetween(1886, 2100);
        RuleFor(v => v.StartingBid).GreaterThanOrEqualTo(0);
    }
}
