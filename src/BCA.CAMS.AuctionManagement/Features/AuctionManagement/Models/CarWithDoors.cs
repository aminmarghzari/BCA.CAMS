namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public abstract class CarWithDoors : Vehicle
{
    public int NumberOfDoors { get; set; }

    protected CarWithDoors(string id, Manufacturer manufacturer, CarModel carModel, int year, decimal startingBid, int numberOfDoors)
        : base(id, manufacturer, carModel, year, startingBid)
    {
        NumberOfDoors = numberOfDoors;
    }
}
