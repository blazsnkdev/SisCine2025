using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Data.Impl;
using SistemaCineMVC.Data.Repo;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Impl;
using SistemaCineMVC.Services.Interfaces;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//REPOSITORIOS
builder.Services.AddScoped <IAsientoRepository,AsientoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IFuncionRepository, FuncionRepository>();
builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();


//SERVICIOS
builder.Services.AddScoped<IFuncionService, FuncionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IAsientoService, AsientoService>();


//BDCONTEXT
var connectionString = builder.Configuration.GetConnectionString("cn1");
builder.Services.AddDbContext<BdCine2025Context>(options =>
    options.UseSqlServer(connectionString));

//UNIT OF WORK AND REPOSITORIES GENERIC
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();






//BREADCRUMS
builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
{
    options.TagName = "nav";
    options.TagClasses = "";
    options.OlClasses = "breadcrumb";
    options.LiClasses = "breadcrumb-item";
    options.ActiveLiClasses = "breadcrumb-item active";

});

// Add services to the container.
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
