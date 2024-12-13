namespace AbacatePay.Models.Customer;

public record CustomerModel
{
    public string Name { get; set; } = null!;
    public string Cellphone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string TaxId { get; set; } = null!;
}
