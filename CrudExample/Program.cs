using Service;
using ServiceContract;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICountrysService, CountryService>();
builder.Services.AddScoped<IpersonSevice, PersonService>();

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
