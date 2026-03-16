using Fintech.NotificationService.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNotificationApi(builder.Configuration);

var app = builder.Build();

app.UseNotificationApi();

app.Run();
