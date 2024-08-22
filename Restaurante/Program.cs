using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion bd
builder.Services.AddDbContext<RestauranteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestauranteDbContext") ?? throw new InvalidOperationException("Conneccion  'RestauranteDbContext' no encontrada")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
    option =>
    {
        //Formulario de logeo
        option.LoginPath = "/Accesos/Index";
        //Tiempo de acceso
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        //Auteticacion denegada
        option.AccessDeniedPath = "/Home/Privacy";
    }
    );
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
    pattern: "{controller=Home}/{action=Index}");

app.Run();
