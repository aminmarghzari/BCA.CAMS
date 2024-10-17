using BCA.CAMS.Application.Common.Exceptions;
using BCA.CAMS.Application.Features.AuctionManagement.Models;
using FluentValidation;

namespace BCA.CAMS.Application.Features.AuctionManagement.Services;

public class AuctionService : IAuctionService
{
    private readonly Dictionary<string, Vehicle> _vehicles = [];
    private readonly Dictionary<string, Auction> _auctionsInventory = [];
    private readonly IValidator<Vehicle> _vehicleValidator;
    private readonly IValidator<Auction> _auctionValidator;

    public AuctionService(IValidator<Vehicle> vehicleValidator, IValidator<Auction> auctionValidator)
    {
        _vehicleValidator = vehicleValidator;
        _auctionValidator = auctionValidator;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        var validationResult = _vehicleValidator.Validate(vehicle);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        if (_vehicles.ContainsKey(vehicle.Id))
        {
            throw new VehicleAlreadyExistsException("Vehicle with the same ID already exists.");
        }

        _vehicles[vehicle.Id] = vehicle;
    }

    public List<Vehicle> SearchVehicles(string type, string manufacturer, string carModel, int? year)
    {
        return _vehicles.Values.Where(v =>
            (string.IsNullOrEmpty(type) || v.GetType().Name == type) &&
            (string.IsNullOrEmpty(manufacturer) || v.Manufacturer.Name == manufacturer) &&
            (string.IsNullOrEmpty(carModel) || v.CarModel.Name == carModel) &&
            (!year.HasValue || v.Year == year.Value)).ToList();
    }

    public void StartAuction(string vehicleId)
    {
        if (!_vehicles.ContainsKey(vehicleId))
        {
            throw new AuctionException("Vehicle does not exist.");
        }

        if (_auctionsInventory.ContainsKey(vehicleId) && _auctionsInventory[vehicleId].IsActive)
        {
            throw new AuctionException("Auction is already active for this vehicle.");
        }

        var auction = new Auction(_vehicles[vehicleId]);

        var validationResult = _auctionValidator.Validate(auction);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        auction.StartAuction();
        _auctionsInventory[vehicleId] = auction;
    }

    public void PlaceBid(string vehicleId, decimal bidAmount, string bidder)
    {
        if (!_auctionsInventory.ContainsKey(vehicleId) || !_auctionsInventory[vehicleId].IsActive)
        {
            throw new AuctionException("Auction is not active for this vehicle.");
        }

        _auctionsInventory[vehicleId].PlaceBid(bidAmount, bidder);
    }

    public void CloseAuction(string vehicleId)
    {
        if (!_auctionsInventory.ContainsKey(vehicleId) || !_auctionsInventory[vehicleId].IsActive)
        {
            throw new AuctionException("Auction is not active for this vehicle.");
        }

        _auctionsInventory[vehicleId].CloseAuction();
    }

    public Auction GetAuction(string vehicleId)
    {
        if (!_auctionsInventory.ContainsKey(vehicleId))
        {
            throw new AuctionException("Auction does not exist for this vehicle.");
        }

        return _auctionsInventory[vehicleId];
    }
}
