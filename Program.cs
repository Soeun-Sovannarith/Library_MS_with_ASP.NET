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



var app = builder.Build();
// Enable serving static files
app.UseStaticFiles();

// Optional: Enable default file mapping (so wwwroot/index.html is served on root "/")
app.UseDefaultFiles();

// Map controllers
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
