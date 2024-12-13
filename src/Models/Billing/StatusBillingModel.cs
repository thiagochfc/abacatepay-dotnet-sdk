using System.Runtime.Serialization;

namespace AbacatePay.Models.Billing;

public enum StatusBillingModel
{
    [EnumMember(Value = "PENDING")]
    Pending,
    [EnumMember(Value = "EXPIRED")]
    Expired,
    [EnumMember(Value = "CANCELLED")]
    Cancelled,
    [EnumMember(Value = "PAID")]
    Paid,
    [EnumMember(Value = "REFUNDED")]
    Refunded
}
