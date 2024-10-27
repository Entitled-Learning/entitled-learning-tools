using System;
using Stripe;
using Stripe.Checkout;

namespace EntitledLearning.Enrollment.UI;

public interface IPaymentProcessor
{
    public Task<Session> CreateCheckoutSession(string baseUri);

    public Task<Session> CreateEnrollmentCheckoutSession(string baseUri, int studentCount);

    public Task<Session?> GetCheckoutSessionAsync(string sessionId);

    public void ProcessPaymentWebhook(string json);
}

