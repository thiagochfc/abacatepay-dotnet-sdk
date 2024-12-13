namespace AbacatePay.Billings;

public sealed class Product
{
    public string ExternalId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Quantity { get; private set; }
    public int Price { get; private set; }

    public Product(string externalId, string name, string description, int quantity, int price)
    {
        ExternalId = externalId;
        Name = name;
        Description = description;
        Quantity = quantity;
        Price = price;
    }

    public Product(string externalId, string name, int quantity, int price)
    {
        ExternalId = externalId;
        Name = name;
        Quantity = quantity;
        Price = price;
    }
}
