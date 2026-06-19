using ColorexApp.Application;
using ColorexApp.Application.Mapping;
using ColorexApp.Infrastructure;
using ColorexApp.Infrastructure.Persistence;
using ColorexApp.WebApi.Extensions;
using ColorexApp.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebApi(builder.Configuration);

var app = builder.Build();

MappingConfig.Configure();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ColorexDbContext>();
    await context.Database.EnsureCreatedAsync();
    await SeedData.SeedAsync(context);
}

await app.RunAsync();
