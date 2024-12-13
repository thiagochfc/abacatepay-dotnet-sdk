using System.Collections.Generic;

namespace AbacatePay.Models;

public sealed class DataArrayModel<T> where T : notnull
{
    public IEnumerable<T> Data { get; set; } = null!;
}
