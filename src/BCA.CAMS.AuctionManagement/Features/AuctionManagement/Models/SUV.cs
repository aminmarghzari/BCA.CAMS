namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public class SUV : CarWithDoors
{
    public SUV(string id, Manufacturer manufacturer, CarModel carModel, int year, decimal startingBid, int numberOfDoors)
        : base(id, manufacturer, carModel, year, startingBid, numberOfDoors)
    {
    }
}