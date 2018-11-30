# Validations

This SDK performs a before-request data validation to make sure your request does not violate certain rules or limits in the MercadoPago API. Things such as string length limits, numeric ranges, required fields, etc. are validated when calling `Save()` or `Update()` methods throughout the SDK, and might throw an exception with a detailed explanation of the validation error, instead of sending the request to the MP API and giving back and unfriendly, description-less HTTP 400 Bad Request response.

This feature is not fully implemented in the official SDK at this moment.