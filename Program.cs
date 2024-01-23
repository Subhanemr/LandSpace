using LandSpace.DAL;
using LandSpace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireDigit= true;

    opt.User.RequireUniqueEmail= true;

    opt.Lockout.MaxFailedAccessAttempts= 5;
    opt.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromSeconds(10);
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = $"/Admin/Account/Login/{opt.ReturnUrlParameter}");
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    initializer.MigrationAsync().Wait();
    initializer.CreateRoleAsync().Wait();
    initializer.CreateAdminAsync().Wait();
}

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "/{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();
