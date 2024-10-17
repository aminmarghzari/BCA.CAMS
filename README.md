# BCA Car Auction Management System

## Overview
BCA Car Auction Management System (CAMS) is designed to manage car auctions, including various vehicle types such as Sedans, SUVs, Hatchbacks, and Trucks. The system allows users to add vehicles, search for vehicles, and manage auctions including starting, placing bids, and closing auctions.

## Project Structure

### Models
- **Vehicle**: Represents a generic vehicle with common attributes like manufacturer, model, year, and starting bid.
- **CarWithDoors**: Inherits from Vehicle and includes the number of doors.
- **Hatchback**: Inherits from CarWithDoors.
- **Sedan**: Inherits from CarWithDoors.
- **SUV**: Inherits from CarWithDoors.
- **Truck**: Inherits from Vehicle and includes the load capacity.
- **Auction**: Manages the auction process for a vehicle, including tracking the current highest bid and bidder.


### Services
- **AuctionService**: Provides services for managing auctions, including adding vehicles, searching for vehicles, starting auctions, placing bids, and closing auctions.

### Validation and Validators

- **VehicleValidator**: Validates vehicle data before adding to the auction inventory.
- **AuctionValidator**: Validates auction data before starting an auction.

### Exceptions
- **BaseException**: Base class for custom exceptions.
- **VehicleAlreadyExistsException**: Raised when adding a vehicle that already exists in the inventory.
- **AuctionException**: Raised for general auction-related errors.

## Testing

### Unit Tests
Unit tests are designed to verify the functionality of individual components in isolation. The unit tests for the BCA Car Auction Management System cover the following areas:
- **AuctionServiceTests**
- **AuctionTests**
- **AuctionValidatorTests**
- **VehicleValidatorTests**


## Main Services

### Adding Vehicles
To add a vehicle, create an instance of the specific vehicle type (e.g., Sedan, SUV) and use the `AddVehicle` method of `AuctionService`.

### Starting an Auction
To start an auction, use the `StartAuction` method of `AuctionService` with the vehicle's ID.

### Placing Bids
To place a bid, use the `PlaceBid` method of `AuctionService` with the vehicle's ID, bid amount, and bidder name.

### Closing an Auction
To close an auction, use the `CloseAuction` method of `AuctionService` with the vehicle's ID.

### Assumptions
Unique Vehicle Identifiers: Each vehicle has a unique identifier, which is used to manage vehicles in the inventory and auctions.

Single Active Auction per Vehicle: At any given time, only one auction can be active for a specific vehicle. This simplifies the management of auctions and bids.

Positive Bid Amounts: Bids must be positive and higher than the current highest bid to be considered valid.

Basic Error Scenarios: The system handles basic error scenarios such as duplicate vehicle IDs, non-existent vehicles, and invalid bid amounts. Additional edge cases may need to be considered in a real-world implementation.





