# Lexim MercadoPago SDK - Change Log

## 1.0.1 (Current)

### Bug fixes:

  - Fixed a serialization bug that would cause an "invalid_items" error when creating a Preference for the first time (fixes https://github.com/mercadopago/dx-dotnet/issues/60)

  - Fixed incorrect endpoints in the `MerchantOrder` resource (fixes https://github.com/mercadopago/dx-dotnet/issues/62)

### New feature: Per request Access Token

  - You can now define a request-specific Access Token for each API call. This allows for scenarios where there are multiple merchants sharing a single web application, which therefore cannot rely on static properties in the `SDK` class.

      See [Usage example](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/MercadoPagoExample/Payments/UserAccessTokenExample.cs) for details.

### Better API interface:

  - Many methods, classes and properties in the SDK have now been marked `internal`, because they're not supposed to be used in user code. This allows for a much better discoverability and Intellisense experience. Many classes have also been marked `sealed` to better indicate that no inheritance is needed.

### New feature: [LINQ Provider](https://github.com/LeximSoluciones/Lexim-MercadoPago-Sdk/blob/master/Docs/Linq.md)

## 1.0.0 (Initial)

Forked from https://github.com/mercadopago/dx-dotnet
