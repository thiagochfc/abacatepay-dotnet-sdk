using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AbacatePay;

public sealed class AbacatePayClient
{
    public readonly AbacatePayClientCustomer Customer;
    public readonly AbacatePayClientBilling Billing;

    public AbacatePayClient(HttpClient httpClient, string token, TimeSpan? timeSpan = null)
    {
        httpClient.BaseAddress = new Uri("https://api.abacatepay.com");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpClient.Timeout = timeSpan ?? TimeSpan.FromSeconds(5);
        Customer = new AbacatePayClientCustomer(httpClient);
        Billing = new AbacatePayClientBilling(httpClient);
    }
}
