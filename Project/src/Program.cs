using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Clients.Helpers;
using Project.Clients.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DockerServer")));

builder.Services.AddControllers();

// Add services to the container.
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
app.UseRouting();
app.UseAuthorization();

// Define a global error handling strategy here if needed
// For simplicity, this example does not include global error handling

app.MapControllers();
app.Run();