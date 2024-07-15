using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoNetTransactionTask;

internal class Program
{
    private const string ConnectionString = @"Data Source=(localdb)\Local;Initial Catalog=TestShop;Integrated Security=true;";

    static void Main(string[] args)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();

        MakeQueryErrorWithTransaction(connection, "лопаты", transaction);

        MakeQueryErrorWithoutTransaction(connection, "ошибки");
    }

    private static void MakeQueryErrorWithTransaction(SqlConnection connection, string categoryName, SqlTransaction transaction)
    {
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
        catch (Exception e)
        {
            transaction.Rollback();

            Console.WriteLine(e.Message);
        }
    }

    private static void MakeQueryErrorWithoutTransaction(SqlConnection connection, string categoryName)
    {
        try
        {
            const string query = "INSERT INTO Category VALUES (@categoryName)";
            using var command = new SqlCommand(query, connection);

            command.Parameters.Add(new SqlParameter("@categoryName", categoryName) { SqlDbType = SqlDbType.NVarChar });
            command.ExecuteNonQuery();

            throw new Exception("Тестовая ошибка без транзакции");

        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
        }
    }
}