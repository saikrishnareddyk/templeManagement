using Microsoft.EntityFrameworkCore;
using TempleCsrApi.Data;
using TempleCsrApi.Repositories;
using TempleCsrApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TempleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDevoteeRepository, DevoteeRepository>();
builder.Services.AddScoped<IDevoteeService, DevoteeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();