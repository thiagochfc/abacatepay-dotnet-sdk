using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AbacatePay.Customers;
using AbacatePay.Models;
using AbacatePay.Models.Customer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbacatePay;

public sealed class AbacatePayClientCustomer
{
    private readonly HttpClient _httpClient;

    internal AbacatePayClientCustomer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool, IEnumerable<CustomerModel>?, ErrorModel?)> ListAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient
            .GetAsync("/v1/customer/list", cancellationToken)
            .ConfigureAwait(false);

        string content = await response
            .Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            DataArrayModel<CustomerModel>? resultCustomer =
                JsonConvert.DeserializeObject<DataArrayModel<CustomerModel>>(content);
            return (resultCustomer is not null, resultCustomer?.Data, null);
        }
        else
        {
            ErrorModel? errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
            return (false, null, errorModel);
        }
    }

    public async Task<(bool, CustomerModel?, ErrorModel?)> CreateAsync(Customer customer,
        CancellationToken cancellationToken)
    {
        using StringContent stringContent =
            new(JsonConvert.SerializeObject(customer, new StringEnumConverter()), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient
            .PostAsync("/v1/customer/create", stringContent, cancellationToken)
            .ConfigureAwait(false);

        string content = await response
            .Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            DataModel<CustomerModel>? resultCustomer = JsonConvert.DeserializeObject<DataModel<CustomerModel>>(content);
            return (resultCustomer is not null, resultCustomer?.Data, null);
        }
        else
        {
            ErrorModel? errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
            return (false, null, errorModel);
        }
    }
}
