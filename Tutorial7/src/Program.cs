using System.Data.SqlClient;
using Tutorial7;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DockerServer")!;
builder.Services.AddSingleton<IWarehouseService>(warehouseService => new WarehouseService(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ExecuteSqlScript("src/Scripts/create.sql", "src/Scripts/proc.sql",  "src/Scripts/drop.sql");

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

void ExecuteSqlScript(string scriptPath1, string scriptPath2, string dropPath)
{
    try
    {
        var createScript = File.ReadAllText(scriptPath1);
        var procScript = File.ReadAllText(scriptPath2);
        var dropScript = File.ReadAllText(dropPath);
        
        using var connection = new SqlConnection(connectionString);
        var command1 = new SqlCommand(dropScript, connection);
        var command = new SqlCommand(createScript, connection);
        var command2 = new SqlCommand(procScript, connection);
        
        connection.Open();
        command1.ExecuteNonQuery();
        command.ExecuteNonQuery();
        command2.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
