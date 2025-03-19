using Microsoft.AspNetCore.Server.Kestrel.Core;
using WeatherDataService.Persistence.Extensions;
using WeatherDataService.Web.GraphQLQuery;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5103, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; 
        listenOptions.UseHttps(); 
    });
});
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddGrpc();
builder.Services.AddScoped<WeatherDataService.Web.Services.WeatherService>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();



var app = builder.Build();

app.UseRouting();

app.MapGrpcService<WeatherDataService.Web.Services.WeatherService>();
app.MapGraphQL((PathString)"/graphql");

app.Run();