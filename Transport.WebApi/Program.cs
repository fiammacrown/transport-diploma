using Microsoft.EntityFrameworkCore;
using Transport.DAL;
using Transport.DAL.Data;
using Transport.DAL.Interfaces;
using Transport.DAL.Repositories;
using Transport.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString)
	//, contextLifetime: ServiceLifetime.Singleton
);

builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<IDeliveriesRepo, DatabaseDeliveriesRepo>();
builder.Services.AddScoped<ITransportsRepo, DatabaseTransportsRepo>();
builder.Services.AddScoped<ILocationsRepo, DatabaseLocationRepo>();
builder.Services.AddScoped<IOrdersRepo, DatabaseOrdersRepo>();
builder.Services.AddScoped<UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
