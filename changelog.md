# Lexim MercadoPago SDK - Change Log

## 1.0.7 (Current)
  - Merged [latest changes](https://github.com/mercadopago/dx-dotnet/tree/7c4bc0eb493b00cd69b7dcdf0a1efb7c360b80fa) from official SDK.
  - `PaymentType.Id` is now nullable. Fixes [#4](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/issues/4) 

## 1.0.6

  - All static methods that perform requests against the MercadoPago API (for instance all `FindById()`, `Search()` and `Query()` methods) now have an `accessToken` parameter, which enables the use of that particular access token for that particular request.

## 1.0.5

  - Implemented `SDK.GetAccessToken(string clientId, string clientSecret)` method, which enables the use of `UserAccessToken` for API calls that traditionally use ClientId and ClientSecret.
  See [Usage example](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/MercadoPagoExample/Payments/UserAccessTokenExample.cs) for details.
  - A critical bug was found in this version, hence it was unlisted from nuget.org.

## 1.0.4

  - Removed incorrect `[StringLength]` validation attribute from property `int? MercadoPago.DataStructures.Preference.Item.CategoryId`. Fixes https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/issues/3

  - Changed all get-only properties from `MercadoPago.Resources.MerchantOrder` to get/private set. Fixes https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/issues/2

  - Reworked validation logic in `MercadoPago.Validation.Validator` for more clear exception messages when validation fails in an unexpected way.

## 1.0.3

  - Removed unneeded Nuget reference: Microsoft.Net.Compilers, which caused an incompatibility with .NET 4.0. Fixes https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/issues/1

## 1.0.2 

  - Merged lastest changes from official version. Introduces a new `PayerType` value: `anonymous`.
  - Improved error message when TLS 1.2 is not enabled

## 1.0.1

### Bug fixes:

  - Fixed a serialization bug that would cause an "invalid_items" error when creating a Preference for the first time (fixes https://github.com/mercadopago/dx-dotnet/issues/60)

  - Fixed incorrect endpoints in the `MerchantOrder` resource (fixes https://github.com/mercadopago/dx-dotnet/issues/62)

### New feature: Per request Access Token

  - You can now define a request-specific Access Token for each API call. This allows for scenarios where there are multiple merchants sharing a single web application, which therefore cannot rely on static properties in the `SDK` class.

      See [Usage example](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/MercadoPagoExample/Payments/UserAccessTokenExample.cs) for details.

### Cleaner API surface:

  - Many methods, classes and properties in the SDK have now been marked `internal`, because they're not supposed to be used in user code. This allows for a much better discoverability and Intellisense experience. Many classes have also been marked `sealed` to better indicate that no inheritance is needed.

### New feature: [LINQ Provider](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/Docs/Linq.md)

## 1.0.0 (Initial)

  - Forked from https://github.com/mercadopago/dx-dotnet
