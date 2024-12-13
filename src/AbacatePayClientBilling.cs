using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AbacatePay.Billings;
using AbacatePay.Models;
using AbacatePay.Models.Billing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbacatePay;

public sealed class AbacatePayClientBilling
{
    private readonly HttpClient _httpClient;

    internal AbacatePayClientBilling(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool, IEnumerable<BillingModel>?, ErrorModel?)> ListAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient
            .GetAsync("/billing/list", cancellationToken)
            .ConfigureAwait(false);

        string content = await response
            .Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            DataArrayModel<BillingModel>? resultBilling =
                JsonConvert.DeserializeObject<DataArrayModel<BillingModel>>(content);
            return (resultBilling is not null, resultBilling?.Data, null);
        }
        else
        {
            ErrorModel? errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
            return (false, null, errorModel);
        }
    }

    public async Task<(bool, BillingModel?, ErrorModel?)> CreateAsync(Billing billing,
        CancellationToken cancellationToken)
    {
        using StringContent stringContent =
            new(JsonConvert.SerializeObject(billing, new StringEnumConverter()), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient
            .PostAsync("/billing/create", stringContent, cancellationToken)
            .ConfigureAwait(false);

        string content = await response
            .Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            DataModel<BillingModel>? resultBilling = JsonConvert.DeserializeObject<DataModel<BillingModel>>(content);
            return (resultBilling is not null, resultBilling?.Data, null);
        }
        else
        {
            ErrorModel? errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
            return (false, null, errorModel);
        }
    }
}
