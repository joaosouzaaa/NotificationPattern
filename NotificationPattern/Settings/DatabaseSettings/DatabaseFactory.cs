using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace NotificationPattern.Settings.DatabaseSettings;

public static class DatabaseFactory
{
    public static void CreateDatabase(string connectionString)
    {
        var databaseCreationScript = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "DatabaseScripts/InitDatabase.sql"));

        using var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        foreach(var script in databaseCreationScript)
        {
            sqlConnection.Execute(script);
        }
    }
}
