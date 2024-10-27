using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EntitledLearning.Enrollment.UI.Controllers.cs;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class StripeController : ControllerBase
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly ILogger<StripeController> _logger;

    public StripeController(IPaymentProcessor paymentProcessor, ILogger<StripeController> logger)
    {
        _paymentProcessor = paymentProcessor;
        _logger = logger;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> PostData()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        if (string.IsNullOrEmpty(json))
        {
            return Ok();
        }

        await _paymentProcessor.ProcessPaymentWebhook(json);

        return Ok();
    }
}

