using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuevakWeb.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QuevakWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuevakWebContext") ?? throw new InvalidOperationException("Connection string 'QuevakWebContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


//session
builder.Services.AddResponseCaching();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
