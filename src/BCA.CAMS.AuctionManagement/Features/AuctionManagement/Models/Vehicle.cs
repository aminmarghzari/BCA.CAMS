using System.ComponentModel.DataAnnotations;

namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public abstract class Vehicle
{
    [Required]
    public string Id { get; set; }

    [Required]
    public Manufacturer Manufacturer { get; set; }

    [Required]
    public CarModel CarModel { get; set; }

    [Range(1886, 2100)]
    public int Year { get; set; }

    [Range(0, double.MaxValue)]
    public decimal StartingBid { get; set; }

    protected Vehicle(string id, Manufacturer manufacturer, CarModel carModel, int year, decimal startingBid)
    {
        Id = id;
        Manufacturer = manufacturer;
        CarModel = carModel;
        Year = year;
        StartingBid = startingBid;
    }
}
