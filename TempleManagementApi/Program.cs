using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Data;
using TempleManagementApi.Interfaces;
using TempleManagementApi.Mappings;
using TempleManagementApi.Repositories;
using TempleManagementApi.Responses;
using TempleManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            var response = ApiResponse<object>.FailureResponse(
                "Validation failed",
                errors
            );

            return new BadRequestObjectResult(response);
        };
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register EF Core DbContext
builder.Services.AddDbContext<TempleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(TempleMappingProfile));

// Register Repositories
builder.Services.AddScoped<IDevoteeRepository, DevoteeRepository>();
builder.Services.AddScoped<ISevaRepository, SevaRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();

// Register Services
builder.Services.AddScoped<IDevoteeService, DevoteeService>();
builder.Services.AddScoped<ISevaService, SevaService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IDonationService, DonationService>();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();