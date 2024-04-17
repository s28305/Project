using System.Data.SqlClient;
using Tutorial6.Models;

namespace Tutorial6.Repositories;

public class AnimalRepository(string connectionString): IAnimalRepository
{ 
    public IEnumerable<Animal> GetAll(string orderBy)
    {
        var animals = new List<Animal>();
        const string queryStr = "SELECT * FROM Animal";

        using var connection = new SqlConnection(connectionString);
        var command = new SqlCommand(queryStr, connection);
        
        connection.Open();
        var reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var animal = ReaderGetAnimal(reader);
                    animals.Add(animal);
                }
            }
        }
        finally 
        {
            reader.Close();
        }

        var sorted = SortAnimals(animals, orderBy);
        
        return sorted;
    }
    
    private static Animal ReaderGetAnimal(SqlDataReader reader)
    {
        return new Animal {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Description = reader.GetString(2),
            Category = reader.GetString(3),
            Area = reader.GetString(4),
        };
    }

    private static IEnumerable<Animal> SortAnimals(IEnumerable<Animal> animals, string orderBy)
    {
        return orderBy.ToLower() switch
        {
            "description" => animals.OrderBy(a => a.Description),
            "category" => animals.OrderBy(a => a.Category),
            "area" => animals.OrderBy(a => a.Area),
            _ => animals.OrderBy(a => a.Name)
        };

    }

    public Animal? GetById(int id)
    {
        const string queryStr = "SELECT * FROM Animal WHERE Id = @Id";
        Animal? animal = null;

        using var connection = new SqlConnection(connectionString);
        var command = new SqlCommand(queryStr, connection);
        command.Parameters.AddWithValue("@Id", id);
            
        connection.Open();
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            animal = ReaderGetAnimal(reader);
        }

        return animal;
    }
    

    public bool Add(Animal animal) 
    {
        const string insertString = "INSERT INTO Animal(Id, Name, Description, Category, Area) " +
                                    "VALUES (@Id, @Name, @Description, @Category, @Area)";
        var rowsCount = -1;
        
        using (var connection = new SqlConnection(connectionString)) 
        {
            SqlCommand command = new(insertString, connection);
            command.Parameters.AddWithValue("Id", animal.Id);
            command.Parameters.AddWithValue("Name", animal.Name);
            command.Parameters.AddWithValue("Description", animal.Description);
            command.Parameters.AddWithValue("Category", animal.Category);
            command.Parameters.AddWithValue("Area", animal.Area);

            connection.Open();
            rowsCount = command.ExecuteNonQuery();
        }

        return rowsCount != -1;
    }

    public bool IdNotExists(int id)
    {
        const string selectString = "SELECT COUNT(*) FROM Animal WHERE Id = @Id";
        using var connection = new SqlConnection(connectionString);
        var selectCommand = new SqlCommand(selectString, connection);
        selectCommand.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        var count = (int)selectCommand.ExecuteScalar();

        return count == 0;
    }
    
    public bool Update(Animal animal)
    {
        const string updateString = """
                                    UPDATE Animal
                                                                      SET Name = @Name,
                                                                          Description = @Description,
                                                                          Category = @Category,
                                                                          Area = @Area
                                                                      WHERE Id = @Id
                                    """;
        var rowCount = -1;
    
        using (var connection = new SqlConnection(connectionString)) 
        {
            SqlCommand command = new(updateString, connection);
            command.Parameters.AddWithValue("@Id", animal.Id);
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Description", animal.Description);
            command.Parameters.AddWithValue("@Category", animal.Category);
            command.Parameters.AddWithValue("@Area", animal.Area);

            connection.Open();
            rowCount = command.ExecuteNonQuery();
        }

        return rowCount > 0;
    }

}