using System;
using EntitledLearning.Enrollment.UI.Models;
using Stripe;
using Stripe.Checkout;

namespace EntitledLearning.Enrollment.UI;

public interface IPaymentProcessor
{
    public Task<Session?> CreateCheckoutSession(string baseUri);

    public Task<Session?> CreateEnrollmentCheckoutSession(string baseUri, string guardianId, IEnumerable<Student> students);

    public Task<Session?> GetCheckoutSessionAsync(string sessionId);

    public Task ProcessPaymentWebhook(string json);
}

