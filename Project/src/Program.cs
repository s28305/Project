using Microsoft.EntityFrameworkCore;
using Project.Helpers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RevenueContext>(options =>
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
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();