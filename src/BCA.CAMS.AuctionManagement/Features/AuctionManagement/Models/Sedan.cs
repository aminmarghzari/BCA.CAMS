namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public class Sedan : CarWithDoors
{
    public Sedan(string id, Manufacturer manufacturer, CarModel carModel, int year, decimal startingBid, int numberOfDoors)
        : base(id, manufacturer, carModel, year, startingBid, numberOfDoors)
    {
    }
}
