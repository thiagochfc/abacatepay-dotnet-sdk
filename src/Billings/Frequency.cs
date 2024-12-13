using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbacatePay.Billings;

public enum Frequency
{
    [JsonConverter(typeof(StringEnumConverter))]
    [EnumMember(Value = "ONE_TIME")]
    OneTime,
}
