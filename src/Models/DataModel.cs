namespace AbacatePay.Models;

public sealed class DataModel<T> where T : notnull
{
    public T Data { get; set; } = default!;
}
