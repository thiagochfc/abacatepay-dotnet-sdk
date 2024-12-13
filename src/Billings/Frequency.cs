using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbacatePay.Billings;

public enum Frequency
{
    [JsonConverter(typeof(StringEnumConverter))]
    OneTime,
}
