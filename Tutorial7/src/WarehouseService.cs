using System.Data.SqlClient;

namespace Tutorial7;

public class WarehouseService(string connectionString): IWarehouseService
{
    public bool ProductExists(int id)
    {
        return ExistsInTable("Product", id);
    }

    public bool WarehouseExists(int id)
    {
        return ExistsInTable("Warehouse", id);
    }

    private bool ExistsInTable(string tableName, int id)
    {
        var queryString = $"SELECT TOP 1 1 FROM {tableName} WHERE Id{tableName} = @Id";

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read(); 
    }


    public int? CheckOrderValidityAndUpdate(int idProduct, int amount, DateTime createdAt)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        const string query =
            "SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt AND FulfilledAt IS NULL";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);

        using var reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        var orderId = reader.GetInt32(0);
        if (UpdateOrder(orderId))
        {
            return orderId;
        }
        return null;
    }

    private bool UpdateOrder(int orderId)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        
        const string query = "UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = @IdOrder";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdOrder", orderId);
        command.Parameters.AddWithValue("@FulfilledAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        
        var rowsAffected = command.ExecuteNonQuery();
        
        return rowsAffected > 0;
    }
    
    public int ProductWarehouseUpdate(int idWarehouse, int idProduct, int amount, int idOrder)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        
        const string query = @"INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                                                VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";

        const string priceQuery = "SELECT Price FROM Product WHERE IdProduct = @ProductId";
        var price = 0.0m;
        using (var priceCommand = new SqlCommand(priceQuery, connection))
        {
            priceCommand.Parameters.AddWithValue("@ProductId", idProduct);

            using (var reader = priceCommand.ExecuteReader())
            {
                if (reader.Read())
                { 
                    price = reader.GetDecimal(0);
                }
            }
        }
        var totalPrice = price * amount;

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
            command.Parameters.AddWithValue("@IdProduct", idProduct);
            command.Parameters.AddWithValue("@IdOrder", idOrder);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@Price", totalPrice);
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            
            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected <= 0) throw new Exception("Failed to insert data.");
            command.CommandText = "SELECT @@IDENTITY";
            var result = command.ExecuteScalar();

            if (result == DBNull.Value) throw new Exception("Failed to insert data.");
            var id = Convert.ToInt32(result);
            return id;

        }
    }
}





        
