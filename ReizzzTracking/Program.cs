using Microsoft.EntityFrameworkCore;
using ReizzzTracking.BL;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.DAL.Common.DbFactory;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.AuthRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ReizzzTrackingV1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReizzzTrackingLocal")));

//Common services

builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//repository, services
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

//builder.Services.AddBl().AddDAL();

builder.Services.AddControllers();
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
