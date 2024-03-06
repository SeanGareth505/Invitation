using InivitationApplication.Models;
using InivitationApplication.Services.Interfaces;
using InivitationApplication.Services.InvertationServices;
using Microsoft.EntityFrameworkCore;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringName)));

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

var app = builder.Build();

// Automatically apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // This line applies the migration
}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();
if (isDevelopment)
{
    // Development-specific middleware can go here
}

app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowAngularDevOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
