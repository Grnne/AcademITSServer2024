using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoNetTransactionTask;

internal class Program
{
    private const string ConnectionString = @"Addr=SEREGA\Grnne;Database=TestShop;Integrated Security=true;TrustServerCertificate=True;";

    static void Main(string[] args)
    {
        try
        {
            MakeQueryErrorWithTransaction(ConnectionString, "лопаты");

            MakeQueryErrorWithoutTransaction(ConnectionString, "ошибки");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void MakeQueryErrorWithTransaction(string connectionString, string categoryName)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();

        try
        {
            const string query = "INSERT INTO Category VALUES (@categoryName)";
            using var command = new SqlCommand(query, connection);

            command.Parameters.Add(new SqlParameter("@categoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
            command.Transaction = transaction;
            command.ExecuteNonQuery();

            throw new Exception("Тестовая ошибка во время транзакции");

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();

            throw;
        }
    }

    private static void MakeQueryErrorWithoutTransaction(string connectionString, string categoryName)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        const string query = "INSERT INTO Category VALUES (@categoryName)";
        using var command = new SqlCommand(query, connection);

        command.Parameters.Add(new SqlParameter("@categoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
        command.ExecuteNonQuery();

        throw new Exception("Тестовая ошибка без транзакции");
    }
}