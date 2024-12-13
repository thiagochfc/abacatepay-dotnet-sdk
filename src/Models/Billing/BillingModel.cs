using System;
using System.Collections.Generic;
using AbacatePay.Billings;

namespace AbacatePay.Models.Billing;

public sealed class BillingModel
{
    public string Id { get; set; } = null!;
    public Uri Url { get; set; } = null!;
    public int Amount { get; set; }
    public StatusBillingModel Status { get; set; }
    public bool DevMode { get; set; }
    public IEnumerable<string> Methods { get; set; } = null!;
    public IEnumerable<ProductBillingModel> Products { get; set; } = null!;
    public Frequency Frequency { get; set; }
    public bool? NextBilling { get; set; }
    public CustomerBillingModel Customer { get; set; } = null!;
}
