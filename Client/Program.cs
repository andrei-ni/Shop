global using DrPrint.Client.Services.AddressService;
global using DrPrint.Client.Services.AuthService;
global using DrPrint.Client.Services.CategoryService;
global using DrPrint.Client.Services.EmailService;
global using DrPrint.Client.Services.OrderService;
global using DrPrint.Client.Services.ProductService;
global using DrPrint.Client.Services.SubCategoryService;
global using DrPrint.Shared;
global using Majorsoft.Blazor.Components.Common.JsInterop;
global using Majorsoft.Blazor.Components.CssEvents;
global using Majorsoft.Blazor.Components.GdprConsent;
global using Majorsoft.Blazor.Extensions.BrowserStorage;
global using Microsoft.AspNetCore.Components.Authorization;
global using System.Net.Http.Json;
using Blazored.LocalStorage;
using DrPrint.Client;
using DrPrint.Client.Services.CartService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddCssEvents();
builder.Services.AddJsInteropExtensions();
builder.Services.AddBrowserStorage();
builder.Services.AddGdprConsent();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();
