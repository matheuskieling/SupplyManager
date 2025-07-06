using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using IntegrationTest.Infra;
using Microsoft.AspNetCore.Mvc;
using SupplyManager.Model;
using SupplyManager.Model.DTO;
using SupplyManager.Model.DTO.Reponses;
using SupplyManager.Model.Enums;

namespace IntegrationTest.ShopTest;

[Collection("IntegrationTests")]
public class ShopTests : IDisposable
{
    private IntegrationTestsFixture<Program> _fixture;
    private Product _product;
    
    public ShopTests()
    {
        _fixture = new IntegrationTestsFixture<Program>();
        _product = _fixture.AddProductAsync(new AddProductRequestDto(
            "Test Product",
            "Test Description",
            1000
        )).Result;

    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public async Task Shop_ReturnsOk_ForValidShopRequest()
    {
        var start = DateTime.UtcNow;
        await _fixture.UpdateProductStockAsync(new UpdateProductStockRequestDto
        {
            ProductId =_product.Id,
            Quantity = 1000,
            StockAction = StockAction.Add
        });
        var request = new ShoppingCartRequestDto
        (
            [
                new ProductOrderRequestDto(
                    _product.Id,
                    10
                )
            ]
        );
        var response = await _fixture.ShopRequestAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var end = DateTime.UtcNow;
        var transaction = await response.Content.ReadFromJsonAsync<TransactionResponseDto>();
        Assert.NotNull(transaction);
        var productOrder = transaction.ShoppingCart.ProductOrders.Single();
        Assert.Equal(_product.Id, productOrder.Product.Id);
        Assert.Equal(request.ProductOrders.First().Quantity, productOrder.Quantity);
        Assert.InRange(transaction.CreatedAt, start, end);
        
        var transactionFromDb = await _fixture.ShopService.GetTransactionByIdAsync(transaction.Id);
        Assert.NotNull(transactionFromDb);
        var transactionProductOrderFromDb = transactionFromDb.ShoppingCart.ProductOrders.Single();
        Assert.Equal(transaction.Id, transactionFromDb.Id);
        Assert.Equal(transaction.ShoppingCart.ProductOrders.Count, transactionFromDb.ShoppingCart.ProductOrders.Count);
        Assert.Equal(productOrder.Product.Id, transactionProductOrderFromDb.Product.Id);
        Assert.Equal(productOrder.Quantity, transactionProductOrderFromDb.Quantity);
        Assert.Equal(productOrder.Product.Name, transactionProductOrderFromDb.Product.Name);
        Assert.Equal(productOrder.Product.Description, transactionProductOrderFromDb.Product.Description);
        Assert.Equal(productOrder.Product.Price, transactionProductOrderFromDb.Product.Price);
    }
    
    [Fact]
    public async Task Shop_ReturnsNotFound_ForShopRequestWithInvalidProductId()
    {
        var request = new ShoppingCartRequestDto
        (
            [
                new ProductOrderRequestDto(
                    Guid.NewGuid(),
                    10
                )
            ]
        );
        var response = await _fixture.ShopRequestAsync(request);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(content);
        Assert.Equal("Product Not Found", content.Title);
    }
    
    [Fact]
    public async Task Shop_ReturnsBadRequest_ForShopRequestWithoutAnyOrders()
    {
        var request = new ShoppingCartRequestDto ([ ]);
        var response = await _fixture.ShopRequestAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(content);
        Assert.Equal("Shopping cart cannot be empty.", content.Detail);
    }
    
    [Fact]
    public async Task Shop_ReturnsBadRequest_ForShopRequestForProductWithoutStock()
    {
        var request = new ShoppingCartRequestDto
        (
            [
                new ProductOrderRequestDto(
                    _product.Id,
                    10
                )
            ]
        );
        var response = await _fixture.ShopRequestAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(content);
        Assert.Equal($"Product stock for product with ID {_product.Id} is empty", content.Detail);
    }
    
    [Fact]
    public async Task Shop_ReturnsBadRequest_ForShopRequestForProductWithoutEnoughStock()
    {
        await _fixture.UpdateProductStockAsync(new UpdateProductStockRequestDto
        {
            ProductId =_product.Id,
            Quantity = 1,
            StockAction = StockAction.Add
        });
        var request = new ShoppingCartRequestDto
        (
            [
                new ProductOrderRequestDto(
                    _product.Id,
                    1000
                )
            ]
        );
        var response = await _fixture.ShopRequestAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(content);
        Assert.Contains($"Insufficient stock", content.Detail);
    }
    
    [Fact]
    public async Task Shop_ReturnsBadRequest_ForShopRequestWithQuantityLessThanEqualsToZero()
    {
        await _fixture.UpdateProductStockAsync(new UpdateProductStockRequestDto
        {
            ProductId =_product.Id,
            Quantity = 1000,
            StockAction = StockAction.Add
        });
        var request = new ShoppingCartRequestDto
        (
            [
                new ProductOrderRequestDto(
                    _product.Id,
                    0
                )
            ]
        );
        var response = await _fixture.ShopRequestAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(content);
        Assert.Equal("Quantity to remove must be greater than zero.", content.Detail);
    }
    
    
}