using Microsoft.EntityFrameworkCore;
using Serilog;
using SyncTask.api.Data;
using SyncTask.api.Repositories;

// Configure Serilog before anything else
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Replace default .NET logger with Serilog
builder.Host.UseSerilog();

// Add basic ASP.NET services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Handle circular references
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories — Dependency Injection
builder.Services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkTaskHourRepository, WorkTaskHourRepository>();


var app = builder.Build();

// Create database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();