using TransfusionAPI.Application;
using TransfusionAPI.Infrastructure;
using TransfusionAPI.Infrastructure.Persistence;
using TransfusionAPI.WebUI;
using TransfusionAPI.WebUI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

Log.Information("Adding services to the container");
// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "/api";
        options.DocumentTitle = "Clean architecture";
    });

    app.UseDeveloperExceptionPage();
    // Executes migrations automatically
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSpa(options =>
{
    options.UseProxyToSpaDevelopmentServer("http://localhost:4200");
});

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }