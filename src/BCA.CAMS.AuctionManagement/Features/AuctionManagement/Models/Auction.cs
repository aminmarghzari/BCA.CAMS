using System.ComponentModel.DataAnnotations;

namespace BCA.CAMS.Application.Features.AuctionManagement.Models;

public class Auction
{
    [Required]
    public Vehicle Vehicle { get; private set; }

    public bool IsActive { get; private set; }

    [Range(0, double.MaxValue)]
    public decimal CurrentHighestBid { get; private set; }

    public string HighestBidder { get; private set; }

    public Auction(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null");
        }

        Vehicle = vehicle;
        IsActive = false;
        CurrentHighestBid = vehicle.StartingBid;
    }

    public void StartAuction()
    {
        if (IsActive)
        {
            throw new InvalidOperationException("Auction is already active.");
        }

        IsActive = true;
    }

    public void CloseAuction()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Auction is not active.");
        }

        IsActive = false;
    }
    public void PlaceBid(decimal bidAmount, string bidder)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Auction is not active.");
        }

        if (bidAmount <= CurrentHighestBid)
        {
            throw new ArgumentException("Bid amount must be higher than the current highest bid.");
        }

        if (bidAmount <= 0)
        {
            throw new ArgumentException("Bid amount must be positive.");
        }

        CurrentHighestBid = bidAmount;
        HighestBidder = bidder;
    }
}
