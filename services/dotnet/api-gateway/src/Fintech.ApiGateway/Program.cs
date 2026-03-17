using Fintech.ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiGatewayServices(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseApiGatewayPipeline();
app.UseAuthorization();
app.UseMiddleware<Fintech.ApiGateway.Middleware.AccessRuleMiddleware>();
app.MapControllers();
app.MapReverseProxy();

app.Run();
