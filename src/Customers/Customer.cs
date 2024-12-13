namespace AbacatePay.Customers;

public sealed class Customer
{
    public string? Name { get; private set; }
    public string? Cellphone { get; private set; }
    public string Email { get; private set; }
    public string? TaxId { get; private set; }

    public Customer(string name, string cellphone, string email, string taxId)
    {
        Name = name;
        Cellphone = cellphone;
        Email = email;
        TaxId = taxId;
    }

    public Customer(string email)
    {
        Email = email;
    }
}
