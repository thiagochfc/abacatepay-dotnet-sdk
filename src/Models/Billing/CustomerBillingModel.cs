namespace AbacatePay.Models.Billing;

public sealed class CustomerBillingModel
{
    public string Id { get; set; } = null!;
    public CustomerMetadataBillingModel Metadata { get; set; } = null!;
}
