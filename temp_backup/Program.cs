using ColorexApp.Application;
using ColorexApp.Infrastructure;
using ColorexApp.WebApi.Extensions;
using ColorexApp.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebApi();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
