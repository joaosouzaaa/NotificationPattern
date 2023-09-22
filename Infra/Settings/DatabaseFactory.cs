using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Settings;

public static class DatabaseFactory
{
    public static void CreateDatabase(string connectionString)
    {
        const string filePath = "/infra/InitDatabase.sql";
        var currentDirectory = Directory.GetCurrentDirectory();
        var directoryList = Directory.GetDirectories(currentDirectory);
        var databaseCreationScript = File.ReadAllLines(Directory.GetCurrentDirectory() + filePath);

        using var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();

        const string databaseExistsScript = @"SELECT COUNT(*)
            FROM sys.databases 
            WHERE name = 'NotificationPattern'";

        var databaseAlreadyExists = sqlConnection.QueryFirstOrDefault<int>(databaseExistsScript);

        if (databaseAlreadyExists is 1)
            return;

        foreach (var script in databaseCreationScript)
        {
            sqlConnection.Execute(script);
        }
    }
}
