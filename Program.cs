using Microsoft.EntityFrameworkCore;
using Library_MS.Data;

var builder = WebApplication.CreateBuilder(args);

// MySQL connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger (API docs)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP → HTTPS
app.UseHttpsRedirection();

// Serve static files from wwwroot
app.UseDefaultFiles();   // looks for index.html
app.UseStaticFiles();

app.UseCors();

// Map API controllers
app.MapControllers();

app.Run();
