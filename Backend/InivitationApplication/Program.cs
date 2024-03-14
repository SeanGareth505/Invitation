using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using InivitationApplication.Models;
using InivitationApplication.Services.Interfaces;
using InivitationApplication.Services.InvertationServices;

var builder = WebApplication.CreateBuilder(args);

// Set up environment-specific configuration
var env = builder.Environment;
var isDevelopment = env.IsDevelopment();
var isProduction = env.IsProduction();

// Load environment-specific configuration file
var appSettingsFile = isDevelopment ? "appsettings.Development.json" : "appsettings.json";
builder.Configuration.AddJsonFile(appSettingsFile, optional: true, reloadOnChange: true);

// Configure your DbContext with the appropriate connection string based on the environment
var connectionStringName = isDevelopment ? "DefaultConnection" : "ProductionConnection";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(connectionStringName));
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services with the dependency injection container
builder.Services.AddTransient<IInvitationService, InvitationService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Define filter function to log all errors
Func<LogEntry, bool> filter = logEntry => logEntry.LogLevel == "Error";

// Register DbLoggerProvider as a logging provider
builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    var serviceProvider = builder.Services.BuildServiceProvider();
    builder.AddProvider(new DbLoggerProvider(filter, serviceProvider.GetRequiredService<ApplicationDbContext>()));
});

var app = builder.Build();

// Configure global exception handling
if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionHandlerFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            if (exceptionHandlerFeature != null)
            {
                var logger = appBuilder.ApplicationServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(exceptionHandlerFeature.Error, "Unhandled exception.");
            }
            await context.Response.WriteAsync("An unexpected server error has occurred.");
        });
    });
}

// Swagger configuration
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDevOrigin");
app.UseAuthorization();
app.MapControllers();

app.Run();
