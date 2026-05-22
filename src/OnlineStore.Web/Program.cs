using FluentValidation;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Application.Services;
using OnlineStore.Application.Validators;
using OnlineStore.Web.Components;
using OnlineStore.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiBaseUrl = builder.Configuration["Api:BaseUrl"]
    ?? throw new InvalidOperationException("Api:BaseUrl is not configured.");

builder.Services.AddHttpClient<IStoreApiClient, StoreApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/");
});

builder.Services.AddValidatorsFromAssemblyContaining<CheckoutValidator>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
