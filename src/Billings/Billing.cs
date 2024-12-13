using System;
using System.Collections.Generic;
using AbacatePay.Customers;

namespace AbacatePay.Billings;

public sealed class Billing
{
    public Frequency Frequency { get; private set; }
    public IEnumerable<Method> Methods { get; private set; }
    public IEnumerable<Product> Products { get; private set; }
    public Uri ReturnUrl { get; private set; }
    public Uri CompletionUrl { get; private set; }
    public string? CustomerId { get; private set; }
    public Customer? Customer { get; private set; }

    public Billing(Frequency frequency,
        IEnumerable<Method> methods,
        IEnumerable<Product> products,
        Uri returnUrl,
        Uri completionUrl,
        Customer? customer)
    {
        Frequency = frequency;
        Methods = methods;
        Products = products;
        ReturnUrl = returnUrl;
        CompletionUrl = completionUrl;
        Customer = customer;
    }

    public Billing(Frequency frequency,
        IEnumerable<Method> methods,
        IEnumerable<Product> products,
        Uri returnUrl,
        Uri completionUrl,
        string customerId)
    {
        Frequency = frequency;
        Methods = methods;
        Products = products;
        ReturnUrl = returnUrl;
        CompletionUrl = completionUrl;
        CustomerId = customerId;
    }
}
