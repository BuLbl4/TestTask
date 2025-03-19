using WeatherProducer.Entry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
