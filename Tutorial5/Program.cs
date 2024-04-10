using Tutorial5.Interfaces;
using Tutorial5.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAnimalRepository, AnimalRepository>();
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

app.MapControllers();

app.Run();
