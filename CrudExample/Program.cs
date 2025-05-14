using Service;
using ServiceContract;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICountrysService, CountryService>();

var app = builder.Build();



app.Run();
