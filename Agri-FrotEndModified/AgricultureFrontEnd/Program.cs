using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AgricultureFrontEnd.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Global authentication policy
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

// Register the auth handler that attaches the JWT token to outgoing requests
builder.Services.AddTransient<BackendApiAuthHandler>();

// Register named HttpClient for API (with JWT auth handler)
builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri("http://agricultureachievement.runasp.net/");
}).AddHttpMessageHandler<BackendApiAuthHandler>();

// Register default HttpClient for direct injection in controllers
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("BackendAPI"));

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
