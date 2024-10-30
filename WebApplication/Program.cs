using Microsoft.EntityFrameworkCore;
using ToDo.BLL.Service;
using ToDo.DAL.DataContext;
using ToDo.DAL.Repositorie;
using ToDo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ArchitectureNLayerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("stringConexionSQL")));

builder.Services.AddScoped<IGenericRepository<Item>, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDo}/{action=ToDo}/{id?}");

app.Run();
