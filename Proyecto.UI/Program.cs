
using Proyecto.UI.Services;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");

builder.Services.AddHttpClient("ApiCliente", client =>
{
    if (string.IsNullOrEmpty(apiBaseUrl))
    {
        throw new InvalidOperationException("La URL base de la API no está configurada en appsettings.json");
    }
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddScoped<ServicioApi>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
