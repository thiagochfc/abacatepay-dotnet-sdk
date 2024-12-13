namespace AbacatePay.Models.Billing;

public sealed class ProductBillingModel
{
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
}
