// Global Imports
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using CAPATHON.Controllers;
global using CAPATHON.Models;
global using CAPATHON.Models.ViewModels;
global using CAPATHON.Data;
global using CAPATHON.Support;

// NonGlobal Imports
using Auth0.AspNetCore.Authentication;

//
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//////////////////////////////////////////////////////////////////////////////

// DATABASE CONNECTION CONFIG

var conn = builder.Configuration.GetConnectionString("HHDBConnection");
builder.Services.AddDbContext<HHDBContext>(q => q.UseSqlServer(conn));

//////////////////////////////////////////////////////////////////////////////

// Cookie configuration for HTTP to support cookies with SameSite=None
builder.Services.ConfigureSameSiteNoneCookies();

// Cookie configuration for HTTPS
//  builder.Services.Configure<CookiePolicyOptions>(options =>
//  {
//     options.MinimumSameSitePolicy = SameSiteMode.None;
//  });

// Auth0 CONNECTION CONFIG
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

//////////////////////////////////////////////////////////////////////////////

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
