using BCA.CAMS.Application.Features.AuctionManagement.Models.Validators;
using BCA.CAMS.Application.Features.AuctionManagement.Models;

using FluentValidation;
using FluentValidation.Results;

namespace BCA.CAMS.UnitTests.AuctionManagement;

public class AuctionValidatorTests
{
    private readonly IValidator<Auction> _auctionValidator;

    public AuctionValidatorTests()
    {
        _auctionValidator = new AuctionValidator();
    }

    [Fact]
    public void Validate_ShouldPass_WhenAuctionIsValid()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        var auction = new Auction(vehicle);
        ValidationResult result = _auctionValidator.Validate(auction);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenVehicleIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => new Auction(null));
        Assert.Equal("Vehicle cannot be null (Parameter 'vehicle')", exception.Message);
    }
}
