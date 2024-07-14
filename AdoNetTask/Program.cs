using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoNetTask;

internal class Program
{
    private const string ConnectionString = @"Data Source=(localdb)\Local;Initial Catalog=TestShop;Integrated Security=true;";

    private static void Main(string[] args)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        Console.WriteLine("Количество товаров: " + GetProductsCount(connection));

        Console.WriteLine("Распечатаем список товаров с помощью SqlReader:");
        PrintProductsListUsingReader(connection);

        Console.WriteLine("Добавим новую категорию и товар, отобразим список с помощью SqlAdapter:");
        InsertCategory(connection, "Шляпы");
        InsertProduct(connection, "PHP", 500, 5);
        PrintProductsListUsingSqlAdapter(connection);

        Console.WriteLine("Удалим первый товар и поменяем имя второму, отобразим список:");
        UpdateProduct(connection, "Кляча", 2);
        DeleteProduct(connection, 1);
        PrintProductsListUsingSqlAdapter(connection);
    }

    private static int GetProductsCount(SqlConnection connection)
    {
        const string query = "SELECT COUNT(*) FROM Product";

        using var command = new SqlCommand(query, connection);

        return (int)command.ExecuteScalar();
    }

    private static void InsertCategory(SqlConnection connection, string categoryName)
    {
        const string query = "INSERT INTO Category VALUES (@categoryName)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.Add(new SqlParameter("@categoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });

        command.ExecuteNonQuery();
    }

    private static void InsertProduct(SqlConnection connection, string productName, decimal productPrice, int productCategoryId)
    {
        const string query = "INSERT INTO Product VALUES (@productName, @productPrice, @productCategoryId)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.Add(new SqlParameter("@productName", productName) { SqlDbType = SqlDbType.NVarChar });
        command.Parameters.Add(new SqlParameter("@productPrice", productPrice) { SqlDbType = SqlDbType.Decimal });
        command.Parameters.Add(new SqlParameter("@productCategoryId", productCategoryId) { SqlDbType = SqlDbType.Int });

        command.ExecuteNonQuery();
    }

    private static void UpdateProduct(SqlConnection connection, string productName, int productId)
    {
        const string query = "UPDATE Product SET Name = @productName WHERE Id = @productId";

        using var command = new SqlCommand(query, connection);

        command.Parameters.Add(new SqlParameter("@productName", productName) { SqlDbType = SqlDbType.NVarChar });
        command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });

        command.ExecuteNonQuery();
    }

    private static void DeleteProduct(SqlConnection connection, int productId)
    {
        const string query = "DELETE FROM Product WHERE Id = @productId";

        using var command = new SqlCommand(query, connection);

        command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });

        command.ExecuteNonQuery();
    }

    private static void PrintProductsListUsingReader(SqlConnection connection)
    {
        const string query = """
                             SELECT p.Name Product, p.Price, c.Name Category
                             FROM Product p
                             INNER JOIN Category c
                             ON p.categoryId = c.Id;
                             """;

        using var command = new SqlCommand(query, connection);

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Категория: {(string)reader["Category"]} Продукт: {(string)reader["Product"]} Цена: {reader["Price"]}");
        }

        reader.Close();
    }

    private static void PrintProductsListUsingSqlAdapter(SqlConnection connection)
    {
        const string query = """
                             SELECT p.Name Product, p.Price, c.Name Category
                             FROM Product p
                             INNER JOIN Category c
                             ON p.categoryId = c.Id;
                             """;

        using var adapter = new SqlDataAdapter(query, connection);
        var dataSet = new DataSet();
        adapter.Fill(dataSet);

        var dataTable = dataSet.Tables[0];

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            Console.WriteLine($"Категория: {cells[2]} Продукт: {cells[0]} Цена: {cells[1]}");
        }
    }
}