
# Unofficial Mercado Pago SDK for .Net

Build Status: 

[![Build Status](https://www.myget.org/BuildSource/Badge/lexim-public?identifier=dd150ea6-14cf-4fc5-ba55-ede6cbf073af)](https://www.myget.org/BuildSource/Badge/lexim-public?identifier=dd150ea6-14cf-4fc5-ba55-ede6cbf073af)


This is an unofficial version of the [MercadoPago SDK for .NET](https://github.com/mercadopago/dx-dotnet).

It has several improvements currently not present in the official version:

  - Bug fixes
  - Per-request Access-Token
  - Better API interface
  - Linq Provider

### .Net versions supported:
  
  - .Net Framework 3.5 and above
  - .Net Standard 2.0

## Installation 

install the Nuget Package Lexim.MercadoPago.Sdk using your favorite package manager.

## Quick Start

### 1. Import the Mercado Pago SDK.
```csharp
using MercadoPago;
```
### 2. Setup your credentials

**For Web-checkout:**
```csharp
MercadoPago.SDK.ClientId = "YOUR_CLIENT_ID";
MercadoPago.SDK.ClientSecret = "YOUR_CLIENT_SECRET";
```
**For API or custom checkout:**
```csharp
MercadoPago.SDK.AccessToken = "YOUR_ACCESS_TOKEN";
```

> **Tip**: You can obtain your MercadoPago credentials [here](https://www.mercadopago.com/mla/account/credentials?type=basic)

### 3. Creating a Preference for basic checkout
    
```csharp
using System.Diagnostics;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;

// Remember to set your MercadoPago credentials! (see step 2)

// Create a preference object
var preference = new Preference
{
    Items =
    {
        new Item
        {
            Id = "1234",
            Title = "Small Silk Plate",
            Quantity = 5,
            CurrencyId = CurrencyId.ARS,
            UnitPrice = 44.23m
        }
    },
    Payer = new Payer
    {
        Email = "augustus_mckenzie@gmail.com"
    }
};

// save to MercadoPago
preference.Save();

// the InitPoint property contains the URL of the web checkout screen for this preference
Process.Start(preference.InitPoint);
```

> You can find more examples in the [`MercadoPagoExample`](MercadoPagoExample) folder.

### 4. Handling Errors

**Error response structure**

![errorstructure](https://user-images.githubusercontent.com/864790/40929584-9cc4c96e-67fb-11e8-80a4-8d797953233a.png)

You can check the errors and causes returned by the API using the `errors` attribute.

```csharp
Console.WriteLine(payment.Errors.Message) // Print the error Message 
```

### Support 

Use the [issues](https://github.com/LeximSoluciones/dx-dotnet/issues) tab if you have any questions or find bugs.
