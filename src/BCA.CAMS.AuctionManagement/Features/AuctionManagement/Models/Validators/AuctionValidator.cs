using FluentValidation;

namespace BCA.CAMS.Application.Features.AuctionManagement.Models.Validators;

public class AuctionValidator : AbstractValidator<Auction>
{
    public AuctionValidator()
    {
        RuleFor(a => a.Vehicle).NotNull();
        RuleFor(a => a.CurrentHighestBid).GreaterThanOrEqualTo(0);
    }
}
