using System;
using Stripe;
using Stripe.Checkout;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace EntitledLearning.Enrollment.UI;

public class PaymentProcessor : IPaymentProcessor 
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
        var metadata = new Dictionary<string, string>
        {
            { "guardianId", "tets" },
            { "guardianEmail", "tets" }
        };

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
                            Name = "Entitled Learning Student Enrollment",
                            Metadata = metadata
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

    public async Task<Session?> GetCheckoutSessionAsync(string sessionId)
    { 
        var service = new SessionService();

        try
        {
            // Retrieve the Checkout Session
            var session = await service.GetAsync(sessionId);

            // Check the session status
            if (session.Status == "complete")
            {
                // The payment has been successfully completed
                return session;
            }
            else
            {
                // Handle other statuses (e.g., pending, expired)
                return null;
            }
        }
        catch (StripeException ex)
        {
            // Handle errors
            _logger.LogError($"Error retrieving Checkout Session: {ex.Message}");
            throw;
        }
    }

    public void ProcessPaymentWebhook(string json)
    {
        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);
            _logger.LogInformation("Received new event: {0}", stripeEvent.Type);

            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Then define and call a method to handle the successful payment intent.
                // handlePaymentIntentSucceeded(paymentIntent);
            }
            else if (stripeEvent.Type == EventTypes.PaymentMethodAttached)
            {
                var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                // handlePaymentMethodAttached(paymentMethod);
            }
            // ... handle other event types
            else
            {
                // Unexpected event type
                _logger.LogWarning("Unhandled event type: {0}", stripeEvent.Type);
            }
        }
        catch (StripeException ex)
        {
            _logger.LogError($"Webhook processing failed: {ex.Message}");
            throw;

        }
        catch (Exception ex)
        {
            _logger.LogError($"Webhook processing failed: {ex.Message}");
            throw;
        }
    }
}

