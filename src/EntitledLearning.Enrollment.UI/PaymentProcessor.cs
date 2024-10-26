using System;
using Stripe;
using Stripe.Checkout;

namespace EntitledLearning.Enrollment.UI;

public class PaymentProcessor
{
    private ILogger<PaymentProcessor> _logger;

    public PaymentProcessor(IConfiguration configuration, ILogger<PaymentProcessor> logger)
    {
        StripeConfiguration.ApiKey = configuration["StripeSecret"];
        _logger = logger;
    }

    public async Task<Session> CreateCheckoutSession(string baseUri)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 2000,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Entitled Learning Student Enrollment"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = $"{baseUri}success",
            CancelUrl = $"{baseUri}cancel"
        };
        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session;
    }

    public async Task<Session> CreateEnrollmentCheckoutSession(string baseUri, int studentCount)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 15000,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Entitled Learning Student Enrollment"
                        }
                    },
                    Quantity = studentCount
                }
            },
            Mode = "payment",
            SuccessUrl = $"{baseUri}payment-success",
            CancelUrl = $"{baseUri}payment-success"
        };

        _logger.LogInformation("made it here");

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session;
    }
}

