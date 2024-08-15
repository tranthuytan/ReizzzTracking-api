using ReizzzTracking.DAL.Common.DbFactory;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Repositories.AuthRepository;

var builder = WebApplication.CreateBuilder(args);
var a = builder.Environment;
// Add services to the container.

builder.Services
    .AddDAL(builder.Configuration)
    .AddBl();

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
