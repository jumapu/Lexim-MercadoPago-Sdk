
# Unofficial Mercado Pago SDK for .Net

Build Status: 

[![Build Status](https://www.myget.org/BuildSource/Badge/lexim-public?identifier=dd150ea6-14cf-4fc5-ba55-ede6cbf073af)](https://www.myget.org/BuildSource/Badge/lexim-public?identifier=dd150ea6-14cf-4fc5-ba55-ede6cbf073af)


This library provides developers with a simple set of bindings to the Mercado Pago API.

### .Net versions supported:
  
  - .Net Framework 3.5 and above
  - .Net Standard 2.0

## Installation 

### Using our nuget package

**Using Package Manager**

`PM> Install-Package Lexim.MercadoPago.Sdk`

**Using .Net CLI**

`> dotnet add package Lexim.MercadoPago.Sdk`

**Using Paket CLI**

`> paket add Lexim.MercadoPago.Sdk`

## Quick Start

### 1. You have to import the Mercado Pago SDK.
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
### 3. Using resource objects

You can interact with all the resources available in the public API, to this each resource is represented by classes according to the following diagram:

![sdk resource structure](https://user-images.githubusercontent.com/864790/34393059-9acad058-eb2e-11e7-9987-494eaf19d109.png)

**Sample (Creating a Payment)**
    
```csharp
using MercadoPago;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Payment;
using MercadoPago.Common;

MercadoPago.SDK.AccessToken = "YOUR_ACCESS_TOKEN";

Payment payment = new Payment
{
    TransactionAmount = 100.0m,
    Token = "YOUR_CARD_TOKEN"
    Description = "Ergonomic Silk Shirt",
    PaymentMethodId = "visa", 
    Installments = 1,
    Payer = new Payer {
        Email = "larue.nienow@hotmail.com"
    }
};

payment.Save();

Console.WriteLine(payment.Status);
```

> You can find more examples in the `MercadoPagoExample` folder.

### 4. Handling Errors

**Error response structure**

![errorstructure](https://user-images.githubusercontent.com/864790/40929584-9cc4c96e-67fb-11e8-80a4-8d797953233a.png)

You can check the errors and causes returned by the API using the `errors` attribute.

```csharp
Console.WriteLine(payment.Errors.Message) // Print the error Message 
```

### Support 

Use the [issues](https://github.com/LeximSoluciones/dx-dotnet/issues) tab if you have any questions or find bugs.
