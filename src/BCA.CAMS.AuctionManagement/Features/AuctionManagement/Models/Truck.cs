namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public class Truck : Vehicle
{
    public int LoadCapacity { get; set; }

    public Truck(string id, Manufacturer manufacturer, CarModel carModel, int year, decimal startingBid, int loadCapacity)
        : base(id, manufacturer, carModel, year, startingBid)
    {
        LoadCapacity = loadCapacity;
    }
}
