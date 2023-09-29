using Azure.Storage.Blobs;
using BodyaFen_spotify_.Contexts;
using BodyaFen_spotify_.Dopomoga;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BodyaFen_spotify_.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration.GetSection("Azure");
builder.Services.Configure<AzureConfig>(config);
builder.Services.AddDbContext<BodyaFenDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDefaultIdentity<Artist>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<BodyaFenDbContext>();
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
app.MapRazorPages();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
