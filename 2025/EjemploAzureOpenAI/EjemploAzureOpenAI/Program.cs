using Mscc.GenerativeAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Configura la inyección de dependencias para el cliente de Gemini.
// La clave de API se toma del archivo appsettings.json.
builder.Services.AddScoped<GoogleAI>(sp =>
{
    var apiKey = builder.Configuration["GoogleAI:ApiKey"];
    return new GoogleAI(apiKey);
});
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
