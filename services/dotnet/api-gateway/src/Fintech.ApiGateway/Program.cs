using Fintech.ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiGatewayServices(builder.Configuration);

var app = builder.Build();

app.UseApiGatewayPipeline();
app.UseAuthorization();
app.MapControllers();
app.MapReverseProxy();

app.Run();
