using BCA.CAMS.Application.Features.AuctionManagement.Models;

namespace BCA.CAMS.UnitTests.AuctionManagement;

public class AuctionTests
{
    private readonly Manufacturer _manufacturer;
    private readonly CarModel _model;
    private readonly Vehicle _vehicle;
    private readonly Auction _auction;

    public AuctionTests()
    {
        _manufacturer = new Manufacturer(1, "BMW");
        _model = new CarModel(1, "X4");
        _vehicle = new Sedan("1", _manufacturer, _model, 2024, 10000, 4);
        _auction = new Auction(_vehicle);
    }

    [Fact]
    public void StartAuction_ShouldSetAuctionToActive()
    {
        _auction.StartAuction();
        Assert.True(_auction.IsActive);
    }

    [Fact]
    public void StartAuction_ShouldThrowException_WhenAuctionIsAlreadyActive()
    {
        _auction.StartAuction();
        Assert.Throws<InvalidOperationException>(() => _auction.StartAuction());
    }

    [Fact]
    public void CloseAuction_ShouldSetAuctionToInactive()
    {
        _auction.StartAuction();
        _auction.CloseAuction();
        Assert.False(_auction.IsActive);
    }

    [Fact]
    public void CloseAuction_ShouldThrowException_WhenAuctionIsNotActive()
    {
        Assert.Throws<InvalidOperationException>(() => _auction.CloseAuction());
    }

    [Fact]
    public void PlaceBid_ShouldUpdateHighestBidAndBidder()
    {
        _auction.StartAuction();
        _auction.PlaceBid(11000, "Bidder1");
        Assert.Equal(11000, _auction.CurrentHighestBid);
        Assert.Equal("Bidder1", _auction.HighestBidder);
    }

    [Fact]
    public void PlaceBid_ShouldThrowException_WhenAuctionIsNotActive()
    {
        Assert.Throws<InvalidOperationException>(() => _auction.PlaceBid(11000, "Bidder1"));
    }

    [Fact]
    public void PlaceBid_ShouldThrowException_WhenBidAmountIsNotHigherThanCurrentBid()
    {
        _auction.StartAuction();
        Assert.Throws<ArgumentException>(() => _auction.PlaceBid(9000, "Bidder1"));
    }
}
