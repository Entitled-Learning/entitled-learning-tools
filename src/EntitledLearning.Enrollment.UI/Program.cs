// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Rewrite;
using Radzen;
using Serilog;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.Repository;
using EntitledLearning.Enrollment.UI;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Build the configuration
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Configure logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(Configuration)
    .CreateLogger();

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
builder.Services.AddSingleton<BlobStoreAdapter>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<GuardianRepository>();
builder.Services.AddScoped<GuardianStudentRelationshipRepository>();
builder.Services.AddScoped<CommunityPartnerRepository>();
builder.Services.AddScoped<CommunityPartnerContactRepository>();
builder.Services.AddScoped<InventoryItemRepository>();
builder.Services.AddScoped<EnrollmentRepository>();
builder.Services.AddScoped<AcademicTermRepository>();
builder.Services.AddTransient<IPaymentProcessor, PaymentProcessor>();
builder.Services.AddSingleton<ISqlDataClient, SqlDataClient>();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<PageNotificationService>();
builder.Services.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseRewriter(
    new RewriteOptions().Add(
        context => { if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
            { context.HttpContext.Response.Redirect("/"); }
        })
);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseSerilogRequestLogging();

app.UseMiddleware<UserToScopeFilter>();

try
{
    Log.Information("Starting Entitled Learning Tools");
    Log.Information("Environment: {environment}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

    app.Run();
}
catch(Exception ex){
    Log.Fatal(ex, "Application failed to start");
}
finally{
    Log.CloseAndFlush();
}

