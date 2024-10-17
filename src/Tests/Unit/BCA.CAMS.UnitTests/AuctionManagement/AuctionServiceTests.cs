using BCA.CAMS.Application.Common.Exceptions;
using BCA.CAMS.Application.Features.AuctionManagement.Models;
using BCA.CAMS.Application.Features.AuctionManagement.Services;

namespace BCA.CAMS.UnitTests.AuctionManagement;

using BCA.CAMS.Application.Features.AuctionManagement.Models.Validators;
using FluentValidation;
using Xunit;

public class AuctionServiceTests
{
    private IAuctionService _auctionService;
    private readonly IValidator<Vehicle> _vehicleValidator;
    private readonly IValidator<Auction> _auctionValidator;

    public AuctionServiceTests()
    {
        _vehicleValidator = new VehicleValidator();
        _auctionValidator = new AuctionValidator();
        _auctionService = new AuctionService(_vehicleValidator, _auctionValidator);
    }

    [Fact]
    public void AddVehicle_ShouldAddVehicle()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        var result = _auctionService.SearchVehicles("Sedan", "BMW", "X4", 2024);
        Assert.Single(result);
    }

    [Fact]
    public void AddVehicle_ShouldThrowException_WhenVehicleAlreadyExists()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        Assert.Throws<VehicleAlreadyExistsException>(() => _auctionService.AddVehicle(vehicle));
    }

    [Fact]
    public void StartAuction_ShouldStartAuction()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        _auctionService.StartAuction("1");
        var auction = _auctionService.GetAuction("1");
        Assert.True(auction.IsActive);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenVehicleDoesNotExist()
    {
        Assert.Throws<AuctionException>(() => _auctionService.StartAuction("999"));
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenAuctionIsAlreadyActive()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        _auctionService.StartAuction("1");
        Assert.Throws<AuctionException>(() => _auctionService.StartAuction("1"));
    }

    [Fact]
    public void PlaceBid_ShouldPlaceBid()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        _auctionService.StartAuction("1");
        _auctionService.PlaceBid("1", 11000, "Bidder1");
        var auction = _auctionService.GetAuction("1");
        Assert.Equal(11000, auction.CurrentHighestBid);
        Assert.Equal("Bidder1", auction.HighestBidder);
    }

    [Fact]
    public void PlaceBid_ShouldThrowException_WhenAuctionIsNotActive()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        Assert.Throws<AuctionException>(() => _auctionService.PlaceBid("1", 11000, "Bidder1"));
    }

    [Fact]
    public void PlaceBid_ShouldThrowException_WhenBidAmountIsNotHigherThanCurrentBid()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        _auctionService.StartAuction("1");
        Assert.Throws<ArgumentException>(() => _auctionService.PlaceBid("1", 9000, "Bidder1"));
    }

    [Fact]
    public void CloseAuction_ShouldCloseAuction()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        _auctionService.StartAuction("1");
        _auctionService.CloseAuction("1");
        var auction = _auctionService.GetAuction("1");
        Assert.False(auction.IsActive);
    }

    [Fact]
    public void CloseAuction_ShouldThrowException_WhenAuctionIsNotActive()
    {
        var vehicle = new Sedan("1", new Manufacturer(1, "BMW"), new CarModel(1, "X4"), 2024, 10000, 4);
        _auctionService.AddVehicle(vehicle);
        Assert.Throws<AuctionException>(() => _auctionService.CloseAuction("1"));
    }

    [Fact]
    public void GetAuction_ShouldThrowException_WhenAuctionDoesNotExist()
    {
        Assert.Throws<AuctionException>(() => _auctionService.GetAuction("999"));
    }
}
