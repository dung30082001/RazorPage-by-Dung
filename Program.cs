using MyRazorPage.Hubs;
using MyRazorPage.Models;
using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
// Bổ sung service làm việc với các Pages vào container Kestrel
builder.Services.AddRazorPages();
builder.Services.Configure<CookiePolicyOptions>(option =>
{
    option.CheckConsentNeeded = context => true;
    option.MinimumSameSitePolicy = SameSiteMode.None;
}
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddDbContext<PRN221_DBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("prn221db"));
});
builder.Services.AddSignalR();

builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(5));
//builder.Services.AddScoped<PRN221DBContext>();

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.UseCookiePolicy();

app.MapRazorPages();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.MapHub<HubServer>("/hubserver");
app.Run();
