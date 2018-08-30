# LINQ Provider

A [LINQ Provider](https://stackoverflow.com/a/1568054/643085) has been implemented, using the [ReLinq](https://github.com/re-motion/Relinq) library, which allows writing LINQ queries against the MercadoPago API.

The provider currently supports expressions of type `& (AND)` and `&& (ANDALSO)` and equality comparisons, in the form `x => x.Property == value`.

This improves over the previous search API which required the use of Dictionaries and was therefore fond of [String Typing](http://wiki.c2.com/?StringlyTyped)

## Before:

```csharp
var dictionary = new Dictionary<string, string>();
dictionary.Add("status", "rejected");
dictionary.Add("external_reference", "12345");
var payments = Payment.Search(dictionary);
```

  - The SDK consumer needs to know the valid key/value combinations 
  - There is no type safety.
  - Approach is rather noisy.

## Now:

```csharp
var externalReference = "1234";
var approvedPayments = 
        Payment.Query()
               .Where(x => x.Status == PaymentStatus.approved && x.ExternalReference == externalReference)
               .ToList();
```

  - C# idiomatic approach
  - Improved type safety
  - Provider resolves the `externalReference` variable to its corresponding value, whether it's a property of an object, a method parameter, a local variable or a constant value
  - Code intent is much more clear and there is a lot less noise.
  - You no longer need to "guess" the valid values or have knowledge about implementation details of the SDK or the MP API.
  - Provider resolves the query and performs a `GET` request to https://api.mercadopago.com/v1/payments/search?access_token=xxxxxxx&status=approved&external_reference=1234 and returns the results as an `IEnumerable<Payment>`.
  - The `Search()` method still exists, for compatibility.