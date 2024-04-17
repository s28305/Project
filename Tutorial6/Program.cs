using Tutorial6;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DockerServer")!;
builder.Services.AddSingleton<AnimalRepository>(animalRepository => new AnimalRepository(connectionString));
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
  //  app.UseSwagger();
   // app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//app.MapControllers();

app.Run();