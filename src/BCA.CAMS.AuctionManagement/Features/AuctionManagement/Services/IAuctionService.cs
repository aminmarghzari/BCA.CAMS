using BCA.CAMS.Application.Features.AuctionManagement.Models;

namespace BCA.CAMS.Application.Features.AuctionManagement.Services;

public interface IAuctionService
{
    void AddVehicle(Vehicle vehicle);
    List<Vehicle> SearchVehicles(string type, string manufacturer, string carModel, int? year);
    void StartAuction(string vehicleId);
    void PlaceBid(string vehicleId, decimal bidAmount, string bidder);
    void CloseAuction(string vehicleId);
    Auction GetAuction(string vehicleId);
}
