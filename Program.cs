using MyRazorPage.Models;

var builder = WebApplication.CreateBuilder(args);
// Bổ sung service làm việc với các Pages vào container Kestrel
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PRN221_DBContext>();
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(5));
//builder.Services.AddScoped<PRN221DBContext>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.MapRazorPages();

app.Run();
