using System;
using CommunityToolkit.Diagnostics;
using EntitledLearning.Data.Repository;
using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Enrollment.UI.Models;
using Stripe;
using Stripe.Checkout;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EntitledLearning.Enrollment.UI;

public class PaymentProcessor : IPaymentProcessor 
{
    private readonly EnrollmentRepository _enrollmentRepository;
    private readonly AcademicTermRepository _academicRepository;
    private readonly ILogger<PaymentProcessor> _logger;

    public PaymentProcessor(EnrollmentRepository enrollmentRepository, AcademicTermRepository academicTermRepository, IConfiguration configuration, ILogger<PaymentProcessor> logger)
    {
        Guard.IsNotNull(configuration["StripeSecret"]);

        StripeConfiguration.ApiKey = configuration["StripeSecret"];
        _enrollmentRepository = enrollmentRepository;
        _academicRepository = academicTermRepository;
        _logger = logger;
    }

    public async Task<Session?> CreateCheckoutSession(string baseUri)
    {
        try { 
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
                SuccessUrl = $"{baseUri}payment-success",
                CancelUrl = $"{baseUri}"
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured diring CreateEnrollmentCheckoutSession");

            return null;
        }
    }

    public async Task<Session?> CreateEnrollmentCheckoutSession(string baseUri, string guardianId, IEnumerable<Student> students)
    {
        try
        {
            var activeEnrollmentTerm = await GetActiveEntollmentTerm();
            var lineItems = new List<SessionLineItemOptions>();

            // Add each student as a line item in the Stripe session
            foreach (var student in students)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 15000, // Assuming a $150 enrollment fee per student
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Enrollment for {student.FirstName} {student.LastName}",
                            Metadata = new Dictionary<string, string>
                            {
                                { "guardianId", guardianId},
                                { "studentId", student.Id! },
                                { "activeEnrollmentTermId", activeEnrollmentTerm.TermId! },
                                { "activeEnrollmentTermName", activeEnrollmentTerm.TermName! }
                            },
                        }
                    },
                    //Price = "price_1QEXEBQegY5BlVwwQXorV6u7",
                    Quantity = 1
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                Metadata = new Dictionary<string, string>
                {
                    { "guardianId", guardianId},
                    { "studentIds", string.Join(",", students.Select(s => s.Id)) }, // Store student IDs as a comma-separated string
                    { "activeEnrollmentTermId", activeEnrollmentTerm.TermId! },
                    { "activeEnrollmentTermName", activeEnrollmentTerm.TermName! }
                },
                SuccessUrl = $"{baseUri}payment-success",
                CancelUrl = $"{baseUri}"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured diring CreateEnrollmentCheckoutSession");

            return null;
        }
    }

    private async Task<AcademicTermStorageContractV1> GetActiveEntollmentTerm()
    {
        var activeTerm = await _academicRepository.GetActiveTerm();

        if(activeTerm is null)
        {
            throw new InvalidOperationException("No active enrollment terms found.");
        }
        else
        {
            return activeTerm;
        }
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

    public async Task ProcessPaymentWebhook(string json)
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
                HandlePaymentIntentSucceeded(paymentIntent);
            }
            else if (stripeEvent.Type == EventTypes.PaymentMethodAttached)
            {
                var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                // handlePaymentMethodAttached(paymentMethod);
            }
            else if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;

                if(session is null)
                {
                    _logger.LogInformation("Session is null some how");
                }
                else
                {
                    await HandleCheckoutSessionCompleted(session);
                }
            }

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

    private async Task HandleCheckoutSessionCompleted(Session session)
    {
        try
        {
            if (session.Metadata.TryGetValue("studentIds", out var studentIdsConcatenated))
            {
                var studentIds = studentIdsConcatenated.Split(',');
                var academicTerm = session.Metadata.TryGetValue("activeEnrollmentTermId", out var academicTermId);

                foreach (var studentId in studentIds)
                {
                    try
                    {
                        _logger.LogInformation(studentId);

                        var enrollment = new EnrollmentStorageContractV1();
                        enrollment.StudentId = studentId;
                        enrollment.TermId = academicTermId;
                        enrollment.EnrollmentDate = DateTimeOffset.UtcNow;
                        enrollment.EnrollmentStatus = EnrollmentStatus.Active.ToString();
                        enrollment.Notes = "";

                        await _enrollmentRepository.UpsertAsync(enrollment);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing student ID {studentId}: {ex.Message}");
                    }
                }
            }
            else
            {
                _logger.LogWarning("Metadata does not contain 'studentIds'");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while handling the checkout session: {ex.Message}");
        }

        await Task.CompletedTask;
    }


    private void HandlePaymentIntentSucceeded(PaymentIntent? paymentIntent)
    {
        
    }
}

