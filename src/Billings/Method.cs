using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbacatePay.Billings;

public enum Method
{
    [JsonConverter(typeof(StringEnumConverter))]
    Pix,
}
