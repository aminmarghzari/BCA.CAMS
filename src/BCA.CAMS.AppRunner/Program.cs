using BCA.CAMS.Application.Features.AuctionManagement.Models;
using BCA.CAMS.Application.Features.AuctionManagement.Models.Validators;
using BCA.CAMS.Application.Features.AuctionManagement.Services;
using FluentValidation;

Console.WriteLine("Hello, World!");

// Define Manufacturers and Models
var bmw = new Manufacturer(1, "BMW");
var x4 = new CarModel(1, "X4");
var x6 = new CarModel(2, "X6");

Manufacturer hyundai = new Manufacturer(2, "Hyundai");
var sonata = new CarModel(3, "Sonata");
var elantra = new CarModel(4, "Elantra");

// Define Vehicles
var vehicles = new List<Vehicle>
{
    new Sedan("1", bmw, x4, 2020, 10000, 4),
    new Hatchback("2", bmw, x6, 2019, 8000, 4),
    new SUV("4", hyundai, elantra, 2022, 20000, 5),
    new Truck("3", hyundai, sonata, 2021, 15000, 1000),
};

// Define AuctionService
IValidator<Vehicle> _vehicleValidator = new VehicleValidator();
IValidator<Auction> _auctionValidator = new AuctionValidator();

var auctionService = new AuctionService(_vehicleValidator, _auctionValidator);

// Add AddVehicle to AuctionService
foreach (var vehicle in vehicles)
{
    auctionService.AddVehicle(vehicle);
}

// Start 
auctionService.StartAuction("1");
auctionService.StartAuction("2");

// PlaceBid
auctionService.PlaceBid("1", 11000, "Bidder1");
auctionService.PlaceBid("1", 12000, "Bidder2");
auctionService.PlaceBid("4", 12000, "Bidder3");

// Close Auction
auctionService.CloseAuction("1");

// Show result
var auction = auctionService.GetAuction("1");
Console.WriteLine($"Auction for Vehicle ID: {auction.Vehicle.Id}");
Console.WriteLine($"Highest Bid: {auction.CurrentHighestBid}");
Console.WriteLine($"Highest Bidder: {auction.HighestBidder}");
