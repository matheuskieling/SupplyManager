using System.Net;
using System.Net.Http.Json;
using IntegrationTest.Infra;
using SupplyManager.Model;
using SupplyManager.Model.DTO;

namespace IntegrationTest.ProductTests;

public class ProductTests : IDisposable
{
    private readonly IntegrationTestsFixture<Program> _fixture;
    
    public ProductTests()
    {
        _fixture = new IntegrationTestsFixture<Program>();
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
    
    [Fact]
    public async Task SaveProduct_ReturnsOk()
    {
        var request = new AddProductRequestDto(
            "Test Product",
            "Test Description",
            100000
        );
        var response = await _fixture.SaveProductAsync(request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(request.Name, product.Name);
        Assert.Equal(request.Description, product.Description);
        Assert.Equal(request.Price, product.Price);
        
        var productsResponse = await _fixture.GetProductsAsync();
        Assert.Equal(HttpStatusCode.OK, productsResponse.StatusCode);
        var productsFromDb = await productsResponse.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(productsFromDb);
        Assert.Single(productsFromDb);
        var productFromDb = productsFromDb.Single();
        Assert.Equal(request.Name, productFromDb.Name);
        Assert.Equal(request.Description, productFromDb.Description);
        Assert.Equal(request.Price, productFromDb.Price);
    }

    [Fact]
    public async Task GetProducts_ReturnsOk()
    {
        var response = await _fixture.GetProductsAsync();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(content);
        Assert.Empty(content);
    }
    
}