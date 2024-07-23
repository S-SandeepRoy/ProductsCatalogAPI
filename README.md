# ProductAPI

## Table of Contents
1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Prerequisites](#prerequisites)
4. [Setup and Installation](#setup-and-installation)
5. [Adding Unit Test Project](#adding-unit-test-project)
6. [Running the Application](#running-the-application)
7. [API Endpoints](#api-endpoints)
8. [Data Model](#data-model)
9. [Service Layer](#service-layer)
10. [Controller](#controller)
11. [Testing](#testing)
12. [Dependency Injection](#dependency-injection)
13. [Error Handling](#error-handling)
14. [Swagger Documentation](#swagger-documentation)
15. [Development Workflow](#development-workflow)
16. [Testing API Endpoints with Postman](#testing-api-endpoints-with-postman)
17. [Contributing](#contributing)
18. [Troubleshooting](#troubleshooting)
19. [License](#license)

## Introduction

ProductAPI is a RESTful API built with ASP.NET Core for managing product information. It provides endpoints for adding, retrieving, and deleting products, using in-memory storage for simplicity.

## Project Structure

The project is structured as follows:

```
ProductAPI/
├── Controllers/
│   └── ProductsController.cs
├── Models/
│   └── Product.cs
├── Services/
│   ├── IProductService.cs
│   └── ProductService.cs
├── Tests/
│   ├── ProductServiceTests.cs
│   └── ProductsControllerTests.cs
├── Program.cs
└── ProductAPI.csproj
```

## Prerequisites

- .NET 6.0 SDK or later
- Git
- An IDE (e.g., Visual Studio, Visual Studio Code)

## Setup and Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/ProductAPI.git
   ```

2. Navigate to the project directory:
   ```
   cd ProductAPI
   ```

3. Restore dependencies:
   ```
   dotnet restore
   ```

4. Build the project:
   ```
   dotnet build
   ```

## Adding Unit Test Project

1. Before Running the Unit test project make sure to inclue the Main Project which is `ProductAPI` is referred properly.

2. After cloning the repo if you found an already existing reference which is showing an error, Then remove that and follow the below steps.

    2.1 Right click on the `Dependencies` from Unit test project and then from the drop down, click on `Add Project Reference`.

    2.2. In the `Reference Manager` window, Under the `Projects` section, click on the `ProductAPI` dll reference.

3. This should resolve the project reference issue.


## Running the Application

1. Run the application:
   ```
   dotnet run --project ProductAPI
   ```

2. The API will start, typically at `https://localhost:5001` and `http://localhost:5000`.

3. Access Swagger UI at `https://localhost:5001/swagger`.

## API Endpoints

- `POST /api/products`: Add a new product
- `GET /api/products`: Retrieve all products
- `DELETE /api/products/{id}`: Delete a product by ID

## Data Model

The `Product` model is defined in `Models/Product.cs`:

```csharp
public class Product
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string FirstName { get; set; }
    [Required, MaxLength(20)]
    public string LastName { get; set; }
    [Required, MaxLength(100)]
    public string Description { get; set; }
    [Range(1, 20)]
    public int Quantity { get; set; }
}
```
## Service Layer

The service layer (`Services/IProductService.cs` and `Services/ProductService.cs`) handles business logic:

- `AddProduct`: Adds a new product
- `GetAllProducts`: Retrieves all products
- `DeleteProduct`: Deletes a product by ID

The `ProductService` uses in-memory storage to manage products.

## Controller

The `ProductsController` (`Controllers/ProductsController.cs`) handles HTTP requests:

- `POST`: Adds a new product
- `GET`: Retrieves all products
- `DELETE`: Deletes a product by ID

## Testing

Unit tests are in the `Tests` folder:

- `ProductServiceTests.cs`: Tests for the `ProductService`
- `ProductsControllerTests.cs`: Tests for the `ProductsController`

Run tests with:
```
dotnet test
```

## Dependency Injection

Dependency injection is configured in `Program.cs`:

```csharp
builder.Services.AddSingleton<IProductService, ProductService>();
```

This allows for easy swapping of implementations and facilitates testing.

## Error Handling

The API uses standard HTTP status codes for error handling:

- 200 OK: Successful operation
- 400 Bad Request: Invalid input
- 404 Not Found: Resource not found
- 500 Internal Server Error: Unexpected server error

## Swagger Documentation

In this API project we are using "Swagger UI" which provides interactive API documentation. Access it at `https://localhost:5001/swagger` when the application is running.

## Development Workflow

1. Make changes to the code.
2. Run tests to ensure nothing is broken:
   ```
   dotnet test
   ```
3. Build the project:
   ```
   dotnet build
   ```
4. Run the application to manually test changes:
   ```
   dotnet run --project ProductAPI
   ```
5. Use Swagger UI or Postman to test API endpoints.

## Testing API Endpoints with Postman

### Testing Endpoints

#### 1. Add a New Product (POST)

1. Set the request type to `POST`
2. Enter the URL: `https://localhost:5001/api/products`
3. Go to the "Headers" tab and add:
   - Key: `Content-Type`
   - Value: `application/json`
4. Go to the "Body" tab:
   - Select `raw`
   - Choose `JSON` from the dropdown
   - Enter the product data:
     ```json
     {
       "firstName": "Sandeep Roy",
       "lastName": "Sunnapu",
       "description": "Car product",
       "quantity": 5
     }
     ```
   - Make sure to not add or change anything under `id`

5. Click "Send"
6. You should receive a `200 OK` response if the product was added successfully

#### 2. Get All Products (GET)

1. Set the request type to `GET`
2. Enter the URL: `https://localhost:5001/api/products`
3. Click "Send"
4. You should receive a `200 OK` response with a JSON array of all products

#### 3. Delete a Product (DELETE)

1. Set the request type to `DELETE`
2. Enter the URL: `https://localhost:5001/api/products/{id}` (replace `{id}` with the actual product ID)
   - Example: `https://localhost:5001/api/products/1`
3. Click "Send"
4. You should receive a `200 OK` response if the product was deleted successfully, or a `404 Not Found` if the product doesn't exist


## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and write tests.
4. Run all tests and ensure they pass.
5. Submit a pull request with a clear description of your changes.

## Troubleshooting

- If you encounter port conflicts, modify `Properties/launchSettings.json` to use different ports.
- Ensure all dependencies are correctly restored with `dotnet restore`.
- Check that you're using the correct .NET SDK version.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
