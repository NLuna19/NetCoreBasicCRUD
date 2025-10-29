using BECommentsAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => 
    builder.AllowAnyOrigin()
           .AllowAnyMethod()   
           .AllowAnyHeader()
   );
});

string? defaultC = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(defaultC))
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
string mySqlConnection = defaultC!;

builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
