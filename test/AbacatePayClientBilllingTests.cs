using System.Net;
using AbacatePay.Billings;
using AbacatePay.Customers;
using AbacatePay.Models;
using AbacatePay.Models.Billing;
using RichardSzalay.MockHttp;

namespace AbacatePay.Test;

public class AbacatePayClientBilllingTests
{
    private readonly MockHttpMessageHandler _mockHttp = new();

    [Fact]
    public async Task ShouldCreateBillingSuccessfully()
    {
        // Arrange
        _mockHttp.When("/v1/billing/create")
            .Respond(HttpStatusCode.OK,
                "application/json",
                """
                {
                  "data": {
                    "id": "bill-5678",
                    "url": "https://pay.abacatepay.com/bill-5678",
                    "amount": 4000,
                    "status": "PENDING",
                    "devMode": true,
                    "methods": [
                      "PIX"
                    ],
                    "products": [
                      {
                        "productId": "prod-1234",
                        "quantity": 2
                      }
                    ],
                    "frequency": "ONE_TIME",
                    "nextBilling": null,
                    "customer": {
                      "id": "cust-9876",
                      "metadata": {
                        "name": "João da Silva",
                        "cellphone": "+5511999999999",
                        "email": "joao.silva@example.com",
                        "taxId": "123.456.789-00"
                      }
                    }
                  }
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");
        Billing billing = new(Frequency.OneTime,
            [Method.Pix],
            [new Product("prod-1234", "product", 1, 100)],
            new Uri("https://pay.abacatepay.com/back"),
            new Uri("https://pay.abacatepay.com/complete"),
            new Customer("customer@email.com"));

        // Act
        (bool isSuccess, BillingModel? billingModel, ErrorModel? errorModel) =
            await abacatePayClient.Billing.CreateAsync(billing, default);
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(billingModel);
        Assert.Null(errorModel);
    }

    [Fact]
    public async Task ShouldListBillingsSuccessfully()
    {
        // Arrange
        _mockHttp.When("/v1/billing/list")
            .Respond(HttpStatusCode.OK,
                "application/json",
                """
                {
                  "data": [
                    {
                      "id": "bill-5678",
                      "url": "https://pay.abacatepay.com/bill-5678",
                      "amount": 4000,
                      "status": "PENDING",
                      "devMode": true,
                      "methods": [
                        "PIX"
                      ],
                      "products": [
                        {
                          "productId": "prod-1234",
                          "quantity": 2
                        }
                      ],
                      "frequency": "ONE_TIME",
                      "nextBilling": null,
                      "customer": {
                        "id": "cust-9876",
                        "metadata": {
                          "name": "João da Silva",
                          "cellphone": "+5511999999999",
                          "email": "joao.silva@example.com",
                          "taxId": "123.456.789-00"
                        }
                      }
                    }
                  ]
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");

        // Act
        (bool isSuccess, IEnumerable<BillingModel>? billingModel, ErrorModel? errorModel) =
            await abacatePayClient.Billing.ListAsync(default);
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(billingModel);
        Assert.Single(billingModel);
        Assert.Null(errorModel);
    }

    [Fact]
    public async Task ShouldNotCreateBillingDueToErrors()
    {
        // Arrange
        _mockHttp.When("/v1/billing/create")
            .Respond(HttpStatusCode.Unauthorized,
                "application/json",
                """
                {
                  "error": "Token de autenticação inválido ou ausente."
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");

        // Act
        (bool isSuccess, BillingModel? billingModel, ErrorModel? errorModel) =
            await abacatePayClient.Billing.CreateAsync(null!, default);
        
        // Assert
        Assert.False(isSuccess);
        Assert.Null(billingModel);
        Assert.NotNull(errorModel);
        Assert.NotNull(errorModel?.Error);
    }

    [Fact]
    public async Task ShouldNotListBillingsDueToErrors()
    {
        // Arrange
        _mockHttp.When("/v1/billing/list")
            .Respond(HttpStatusCode.Unauthorized,
                "application/json",
                """
                {
                  "error": "Token de autenticação inválido ou ausente."
                }
                """);
        AbacatePayClient abacatePayClient = new(_mockHttp.ToHttpClient(), "token");

        // Act
        (bool isSuccess, IEnumerable<BillingModel>? billingModel, ErrorModel? errorModel) =
            await abacatePayClient.Billing.ListAsync(default);
        
        // Assert
        Assert.False(isSuccess);
        Assert.Null(billingModel);
        Assert.NotNull(errorModel);
        Assert.NotNull(errorModel?.Error);
    }
}
