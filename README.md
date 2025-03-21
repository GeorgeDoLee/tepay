# TePay

TePay is a .NET library that provides an easy-to-use interface for integrating with the TBC Bank Payment API. It supports various payment operations such as initiating payments, retrieving payment details, canceling payments, handling recurring payments, and authentication.

Before using this project, **please refer to the official TBC Bank API documentation** to understand the API structure and operations:
[TBC Bank API Documentation](https://developers.tbcbank.ge/docs/checkout-api-overview)

## Dependencies

This library uses the following dependencies:

- **[Serilog](https://serilog.net/)** – Used for logging API requests and responses.
- **[FluentValidation](https://docs.fluentvalidation.net/en/latest/)** – Used for validating input requests before making API calls.

## Features

- Secure authentication with TBC Bank

- Create and manage payments

- Handle recurring payments

- Cancel payments

- Logging with Serilog for detailed request and response tracking

- Input validation for requests using FluentValidation


  

## Configuration

  

Before using the library, configure the API credentials and settings in your application:

  

```csharp
var  config = new  TePayConfig
{
	BaseUrl = "https://api.tbcbank.ge/",   // set on default
	Version = "v1",                        // set on default
	ApiKey = "your-api-key",
	ClientId = "your-client-id",
	ClientSecret = "your-client-secret"
};
```

  

## Usage

  

### Initializing the Service

  

To use `TePayService`, instantiate it with the configuration:

  

```csharp
ITePayService  tePayService = new  TePayService(config);
```

  

### Creating a Payment


**minimal request sample**
```csharp
var request = new CreatePaymentRequest
{
		Amount = new Amount
    {
        Currency = "GEL",
        Total = 1,
    },
    ReturnUrl = "https://test.ge/ReturnURL",
    CallbackUrl = "https://test.ge/CallbackURL",
    PreAuth = false,
    Language = "EN",
    MerchantPaymentId = "P123123",
    Description = "Test Description"
};

var response = await service.CreatePaymentAsync(request);
```

**extended request sample**
```csharp
var request = new CreatePaymentRequest
{
    Amount = new Amount
    {
        Currency = "GEL",
        Total = 1,
        SubTotal = 0,
        Tax = 0,
        Shipping = 0
    },
    ReturnUrl = "https://test.ge/ReturnURL",
    Extra = "GE60TB4572261006330008",
    UserIpAddress = "127.0.0.1",
    ExpirationMinutes = 12,
    Methods = new List<PaymentMethod>
    {
        PaymentMethod.Pan,
        PaymentMethod.InternetBankLogin,
        PaymentMethod.Installment
    },
    InstallmentProducts = new List<InstallmentProduct>
    {
        new InstallmentProduct
        {
            Name = "t1",
            Price = 100,
            Quantity = 1
        },
        new InstallmentProduct
        {
            Name = "t1",
            Price = 50,
            Quantity = 1
        },
        new InstallmentProduct
        {
            Name = "t1",
            Price = 50,
            Quantity = 1
        }
    },
    CallbackUrl = "https://test.ge/CallbackURL",
    PreAuth = false,
    Language = "KA",
    MerchantPaymentId = "P123123",
    SaveCard = true,
    SaveCardToDate = "1030",
    Description = "Test Description"
};

var response = await service.CreatePaymentAsync(request);
```

### Get Payment Details

```csharp
var response = await service.GetPaymentDetailsAsync("payId");
```

### Canceling a Payment

  

```csharp
var request = new CancelPaymentRequest
{
    Amount = 2,
    Extra = "GE42TB0000000000000000000000", // IBAN format for split transactions (optional)
    Extra2 = "10.50"  			    // Amount to be canceled for a split transaction (optional)
};

await service.CancelPaymentAsync("payId", request);
```

  

### Completing a Pre-Authorized Payment

  

```csharp
decimal amount = 100;
var response = await service.CompletePreAuthPaymentAsync("payId", amount);
```

  

### Executing a Recurring Payment

  

```csharp
var request = new ExecuteRecurringPaymentRequest
{
    Money = new Money
    {
        Amount = 10,
        Currency = "GEL",
    },
    RecID = "rec_id",
};

await service.ExecuteRecurringPaymentAsync(request);
```
  
### Deleting a Recurring Payment

  
```csharp
await  service.DeleteRecurringPaymentAsync("recID");
```
  

## Error Handling

When an error occurs, the API provides an `ErrorResponse` object with details about the error. This object is returned in exceptions such as TePayApiException or TePayAuthenticationException.

Here is the structure of the ErrorResponse:

```csharp
public class ErrorResponse
{
    public string? Type { get; set; }

    public string? Title { get; set; }

    public int? Status { get; set; }

    public string? Detail { get; set; }

    public string? SystemCode { get; set; }

    public string? Code { get; set; }

    public string? ResultCode { get; set; }

    public string? TraceId { get; set; }
}
```

The library provides custom exceptions for handling different types of errors:

-  `TePayAuthenticationException` – Thrown when authentication fails. Includes `ErrorResponse` object.

-  `TePayValidationException` – Thrown when a request validation fails. Includes List of error messages.

-  `TePayApiException` – Thrown when an API error occurs. Includes `ErrorResponse` object

-  `TePaySerializationException` – Thrown when JSON serialization or deserialization fails.

Example:

  

```csharp
try
{
	var  payment = await  tePayService.CreatePaymentAsync(request);
}
catch (TePayAuthenticationException  ex)
{
	var errorResponse = ex.ErrorResponse;
	Console.WriteLine("Authentication Error: " + errorResponse.Title);
}
catch (TePayApiException  ex)
{
	var errorResponse = ex.ErrorResponse;
	Console.WriteLine("API Error: " + errorResponse.Title);
}
catch (TePayValidationException ex)
{
	foreach (var error in ex.Errors)
	{
		Console.WriteLine($"Error: {error}");
	}
	Console.WriteLine("Validation Error: " + ex.Message);
}
catch (Exception  ex)
{
	Console.WriteLine("Unexpected Error: " + ex.Message);
}
```

## Validators

  
The TePay library uses FluentValidation for input validation to ensure data integrity before making API requests. The main request models have specific validation rules for fields such as `Amount`, `ReturnUrl`, `Extra`, `Extra2`, and more.

Example validation rules:

-  **CancelPaymentRequest**: Ensures the `Amount` is greater than zero and that `Extra` follows a valid IBAN format.

-  **CreatePaymentRequest**: Validates fields like `ReturnUrl`, `Extra`, `Extra2`, `UserIpAddress`, and payment methods, including ensuring proper format and constraints.

-  **ExecuteRecurringPaymentRequest**: Ensures that required fields like `RecID` and `Money` are set, with validation on parameters like `Extra` and `Extra2`.

Refer to the validators in the `TePay.Validators` namespace for more details.


## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contributing

Contributions are welcome! Feel free to open issues and pull requests.
Contributions are welcome! Feel free to open issues and pull requests.  
Please read our [Contribution Guidelines](CONTRIBUTING.md) before submitting changes.
