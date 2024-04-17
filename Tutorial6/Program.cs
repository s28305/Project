using System.Data.SqlClient;
using Tutorial6.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DockerServer")!;
builder.Services.AddSingleton<IAnimalRepository>(animalRepository => new AnimalRepository(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ExecuteSqlScript("Scripts/createAnimal.sql", "Scripts/drop.sql");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
return;

void ExecuteSqlScript(string scriptPath, string dropPath)
{
    try
    {
        var createScript = File.ReadAllText(scriptPath);
        var dropScript = File.ReadAllText(dropPath);
        using var connection = new SqlConnection(connectionString);
        var command1 = new SqlCommand(dropScript, connection);
        var command = new SqlCommand(createScript, connection);
        connection.Open();
        command1.ExecuteNonQuery();
        command.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}