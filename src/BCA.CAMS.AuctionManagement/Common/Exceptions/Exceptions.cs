namespace BCA.CAMS.Application.Common.Exceptions;

public class BaseException : Exception
{
    public BaseException(string message) : base(message) { }
}

public class VehicleAlreadyExistsException : BaseException
{
    private const string DefaultMessage = "Vehicle with the same ID already exists.";

    public VehicleAlreadyExistsException() : base(DefaultMessage) { }

    public VehicleAlreadyExistsException(string message) : base(message) { }
}

public class AuctionException : BaseException
{
    private const string DefaultMessage = "An error occurred with the auction.";

    public AuctionException() : base(DefaultMessage) { }

    public AuctionException(string message) : base(message) { }
}
