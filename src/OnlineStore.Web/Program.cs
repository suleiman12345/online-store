using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineStore.Web;
using OnlineStore.Web.Models;
using OnlineStore.Web.Services;
using OnlineStore.Web.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["Api:BaseUrl"] ?? "http://localhost:5080";

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/"),
});

builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateFormValidator>();
builder.Services.AddScoped<ICartIdService, CartIdService>();
builder.Services.AddScoped<ICollectionApiService, CollectionApiService>();
builder.Services.AddScoped<IProductApiService, ProductApiService>();
builder.Services.AddScoped<ICartApiService, CartApiService>();
builder.Services.AddScoped<IOrderApiService, OrderApiService>();

await builder.Build().RunAsync();
