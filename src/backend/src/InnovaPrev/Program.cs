using Microsoft.AspNetCore.Localization;
using System.Globalization;
using SimpleInjector;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Sets up the basic configuration that for integrating Simple Injector with
// ASP.NET Core by setting the DefaultScopedLifestyle, and setting up auto
// cross wiring.
var container = new SimpleInjector.Container();
builder.Services.AddSimpleInjector(container, options =>
{
    // AddAspNetCore() wraps web requests in a Simple Injector scope and
    // allows request-scoped framework services to be resolved.
    options.AddAspNetCore()

        // Ensure activation of a specific framework type to be created by
        // Simple Injector instead of the built-in configuration system.
        // All calls are optional. You can enable what you need. For instance,
        // ViewComponents, PageModels, and TagHelpers are not needed when you
        // build a Web API.
        .AddControllerActivation();
});

// Registration rules here
CompositionRoot.CustomBindings.Bind(container);

var app = builder.Build();

// UseSimpleInjector() finalizes the integration process.
app.Services.UseSimpleInjector(container);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (localizeOptions != null)
{
    localizeOptions.Value.SetDefaultCulture("it-IT");
    localizeOptions.Value.DefaultRequestCulture.Culture.NumberFormat.CurrencyDecimalDigits = 2;
    localizeOptions.Value.DefaultRequestCulture.Culture.NumberFormat.NumberDecimalDigits = 2;
    app.UseRequestLocalization(localizeOptions.Value);
};

// Always verify the container
container.Verify();

app.Run();
