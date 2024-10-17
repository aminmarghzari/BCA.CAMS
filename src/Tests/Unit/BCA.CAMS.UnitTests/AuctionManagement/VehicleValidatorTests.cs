using BCA.CAMS.Application.Features.AuctionManagement.Models;
using BCA.CAMS.Application.Features.AuctionManagement.Models.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace BCA.CAMS.UnitTests.AuctionManagement;

public class VehicleValidatorTests
{
    private readonly IValidator<Vehicle> _vehicleValidator;

    public VehicleValidatorTests()
    {
        _vehicleValidator = new VehicleValidator();
    }

    [Fact]
    public void Validate_ShouldPass_WhenVehicleIsValid()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        ValidationResult result = _vehicleValidator.Validate(vehicle);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ShouldFail_WhenIdIsEmpty()
    {
        var vehicle = new Sedan("", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        ValidationResult result = _vehicleValidator.Validate(vehicle);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }

    [Fact]
    public void Validate_ShouldFail_WhenYearIsOutOfRange()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2200, 10000, 4);
        ValidationResult result = _vehicleValidator.Validate(vehicle);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Year");
    }

    [Fact]
    public void Validate_ShouldFail_WhenStartingBidIsNegative()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, -10000, 4);
        ValidationResult result = _vehicleValidator.Validate(vehicle);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "StartingBid");
    }
}
