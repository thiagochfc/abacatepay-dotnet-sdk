using System.IO.IsolatedStorage;
using System.Net;
using System.Runtime.InteropServices;
using AbacatePay.Customers;
using AbacatePay.Models;
using AbacatePay.Models.Customer;
using RichardSzalay.MockHttp;

namespace AbacatePay.Test;

public class AbacatePayClientCustomerTests
{
    private readonly MockHttpMessageHandler _mockHttp = new();


    [Fact]
    public async Task ShouldCreateCustomerSuccessfully()
    {
        // Arrange
        _mockHttp.When("/customer/create")
            .Respond(HttpStatusCode.OK,
                "application/json",
                """
                {
                  "data": {
                    "name": null,
                    "cellphone": null,
                    "email": "daniel_lima@abacatepay.com",
                    "taxId": null
                  }
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");
        Customer customer = new("customer@email.com");

        // Act
        (bool isSuccess, CustomerModel? customerModel, ErrorModel? errorModel) =
            await abacatePayClient.Customer.CreateAsync(customer, default);

        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(customerModel);
        Assert.NotNull(customerModel.Email);
        Assert.NotEqual(string.Empty, customerModel.Email);
        Assert.Null(errorModel);
    }

    [Fact]
    public async Task ShouldListCustomersSuccessfully()
    {
        // Arrange
        _mockHttp.When("/customer/list")
            .Respond(HttpStatusCode.OK,
                "application/json",
                """
                {
                  "data": [
                    {
                      "name": "Daniel Lima",
                      "cellphone": "(11) 4002-8922",
                      "email": "daniel_lima@abacatepay.com",
                      "taxId": "123.456.789-01"
                    }
                  ]
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");
        
        // Act
        (bool isSuccess, IEnumerable<CustomerModel>? customerModel, ErrorModel? errorModel) =
            await abacatePayClient.Customer.ListAsync(default);
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(customerModel);
        Assert.Single(customerModel);
        Assert.Null(errorModel);
    }
    
    [Fact]
    public async Task ShouldNotCreateCustomerDueToErrors()
    {
        // Arrange
        _mockHttp.When("/customer/create")
            .Respond(HttpStatusCode.Unauthorized,
                "application/json",
                """
                {
                  "error": "Token de autenticação inválido ou ausente."
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");

        // Act
        (bool isSuccess, CustomerModel? customerModel, ErrorModel? errorModel) =
            await abacatePayClient.Customer.CreateAsync(null!, default);

        // Assert
        Assert.False(isSuccess);
        Assert.Null(customerModel);
        Assert.NotNull(errorModel);
        Assert.NotNull(errorModel?.Error);
    }
    
    [Fact]
    public async Task ShouldNotListCustomersDueToErrors()
    {
        // Arrange
        _mockHttp.When("/customer/list")
            .Respond(HttpStatusCode.Unauthorized,
                "application/json",
                """
                {
                  "error": "Token de autenticação inválido ou ausente."
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");

        // Act
        (bool isSuccess, IEnumerable<CustomerModel>? customerModel, ErrorModel? errorModel) =
            await abacatePayClient.Customer.ListAsync(default);

        // Assert
        Assert.False(isSuccess);
        Assert.Null(customerModel);
        Assert.NotNull(errorModel);
        Assert.NotNull(errorModel?.Error);
    }
}
