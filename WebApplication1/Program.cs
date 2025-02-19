using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using WebApplication1.Data;
using WebApplication1.Services; // Certifique-se de que o namespace está correto para SeedingService e SellerService

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("WebApplication1Context"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("WebApplication1Context"))));

// Registro dos serviços como Scoped
builder.Services.AddScoped<SeddingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartamentService>();
builder.Services.AddScoped<SalesRecordsService>();

// Adiciona os controladores com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("3");
    var enUS = new CultureInfo("en-US");
    var localizationOption = new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(enUS),
        SupportedCultures = new List<CultureInfo> { enUS },
        SupportedUICultures = new List<CultureInfo> { enUS }
    };
    app.UseRequestLocalization(localizationOption);
    var seedingService = scope.ServiceProvider.GetRequiredService<SeddingService>();
    seedingService.Seed();
    Console.WriteLine("4");
}

// Configure o pipeline de requisição HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
