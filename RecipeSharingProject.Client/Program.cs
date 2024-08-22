using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using RecipeSharingProject.Client.Clients;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizePage("/Index");
});

string subscriptionKey = Environment.GetEnvironmentVariable("SUBSCRIPTION_KEY");

builder.Services.AddHttpClient<RecipeClient>(client =>
{
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{subscriptionKey}");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseCookies = true, // Ensure cookies are used
    CookieContainer = new CookieContainer() // Share cookies across requests
});

string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Login";
}).AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = clientId;
    microsoftOptions.ClientSecret = clientSecret;
    microsoftOptions.CallbackPath = "/signin-microsoft";
});

builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
