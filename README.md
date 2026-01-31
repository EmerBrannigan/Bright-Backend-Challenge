This project implements a simple supermarket checkout system that supports
per-item pricing and multi-buy special offers.

## Requirements
- Items identified by SKU (single letters)
- Unit pricing per SKU
- Optional special pricing (e.g. 3 for 130)
- Items can be scanned in any order
- Pricing rules are injected per checkout instance

## Tech Stack
- .NET 8
- C#
- xUnit for unit testing

## Design Decisions
- Checkout implemented as a class library for testability
- Pricing rules passed into the constructor to support changing offers
- Order-independent pricing via internal SKU counting
- Special offers applied greedily per SKU

## Running the Tests
```bash
dotnet test