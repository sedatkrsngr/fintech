using Fintech.ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiGatewayServices(builder.Configuration);

var app = builder.Build();

app.UseApiGatewayPipeline();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
