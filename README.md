# SupplyManager

SupplyManager is a C# application designed to manage product inventory, shopping transactions, and stock updates. It provides a RESTful API for managing products, shopping carts, and transactions, and includes integration tests to ensure reliability.

## Main Goal of the Project
The goal of the project was to study and implement a .NET backend that used a postgreSQL database for testing. In other projects I used TestContainers, and in this one I decided to use a dedicated database with Docker Compose.

## Features

- **Product Management**: Add, retrieve, and update product information.
- **Shopping Transactions**: Create shopping transactions with product orders.
- **Stock Management**: Update product stock with validation for stock availability.
- **Integration Tests**: Comprehensive tests for API endpoints and business logic.

## Technologies Used

- **Languages**: C#
- **Frameworks**: ASP.NET Core, Entity Framework Core
- **Database**: PostgreSQL
- **Testing**: xUnit, Integration Tests
- **Containerization**: Docker Compose

## Project Structure

- **SupplyManager**: Contains the main application code, including controllers, services, and data access layers.
- **IntegrationTest**: Contains integration tests for the application.
- **Database Initialization**: PostgreSQL database is initialized using `init.sql` via Docker Compose.

## Prerequisites

- .NET 8
- Docker and Docker Compose
- PostgreSQL

## Getting Started

1. Clone the repository:
```
SSH: git clone git@github.com:matheuskieling/SupplyManager.git
HTTPS: git clone https://github.com/matheuskieling/SupplyManager.git
```
2. Build the database using Docker Compose:
```
docker-compose up -d
```
3. Run the application
4. Use the API endpoints to manage products and transactions.

## API Endpoints

### ProductController

- `GET /Product`: Retrieve all products.
- `POST /Product`: Add a new product.
- `POST /Product/UpdateProductStock`: Update product stock.

### ShopController

- `POST /Shop`: Create a shopping transaction.

## Database Configuration

The database is configured in the `compose.yaml` file. By default, it uses the following credentials:

- **User**: `admin`
- **Password**: `root`
- **Database**: `supplymanager_db`

You can modify these values in the `compose.yaml` file.

## Running Tests

To run the integration tests:

1. Navigate to the `IntegrationTest` directory.
2. Run the tests using the following command:
```
dotnet test
```

