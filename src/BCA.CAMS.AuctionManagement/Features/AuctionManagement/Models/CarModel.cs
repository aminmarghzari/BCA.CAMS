namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public class CarModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CarModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
