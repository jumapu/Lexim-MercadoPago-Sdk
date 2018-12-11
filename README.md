
# Unofficial Mercado Pago SDK for .NET

![](https://img.shields.io/nuget/v/Lexim.MercadoPago.Sdk.svg)

Build Status:

[![Build Status](https://dev.azure.com/lexim1/LeximLibs/_apis/build/status/Lexim%20Mercado%20SDK%20(GitHub))](https://dev.azure.com/lexim1/LeximLibs/_build/latest?definitionId=1)

This is an unofficial version of the [MercadoPago SDK for .NET](https://github.com/mercadopago/dx-dotnet).

It has several improvements currently not present in the official version:

  - .NET Standard 2.0 support
  - [Bug fixes](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/changelog.md#bug-fixes)
  - [Per-request Access-Token](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/changelog.md#new-feature-per-request-access-token)
  - [Cleaner API surface](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/changelog.md#cleaner-api-surface)
  - [LINQ Provider](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/Docs/Linq.md)
  - A Simpler, cleaner and safer [IPN Notification Handler](https://github.com/mercadopago/dx-dotnet/pull/58)
  - Before-request [Data Validations](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/Docs/Validations.md)
  - [Improved error handling](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/changelog.md#improved-error-handling)
  - [Async support](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/changelog.md#async-support) (.NET Standard / .NET Core only)

### .NET versions supported:

  - [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) and all platforms that support it (.NET Core, Mono, Xamarin, Unity, etc)
  - .NET Framework 4.0 and above on Windows platforms
  - Legacy 1.x versions of the SDK support .NET Framework 3.5

## Installation 

Install Nuget Package [Lexim.MercadoPago.Sdk](https://www.nuget.org/packages/Lexim.MercadoPago.Sdk/) using your favorite package manager.

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

### 3. Creating a Preference for Web (a.k.a "basic") checkout
    
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

> You can find more examples in the [`MercadoPagoSDK.Samples`](MercadoPagoSDK.Samples) folder.

### 4. Handling Errors

There are 3 kinds of errors that you may get when interacting with this SDK:

  - All Non-successful HTTP responses from the MercadoPago API will result in an exception.
  - HTTP responses in the range 400-500 will result in an exception with an `Error` property, with the following structure:

    ![errorstructure](https://user-images.githubusercontent.com/864790/40929584-9cc4c96e-67fb-11e8-80a4-8d797953233a.png)

  - Before-request Validation errors will result in an exception with a clear, detailed message about the relevant resource and property.

> See the [Handling Exceptions Example](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/MercadoPagoSDK.Samples/Samples/ExceptionSample.cs) for details.

### Support 

Use the [issues](https://github.com/LeximSoluciones/dx-dotnet/issues) tab if you have any questions or find bugs.
