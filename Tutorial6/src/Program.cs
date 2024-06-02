using Microsoft.EntityFrameworkCore;
using Tutorial6.Helpers;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnimalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DockerServer")));
builder.Services.AddDbContext<VisitContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DockerServer")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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