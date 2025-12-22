using System.Data;
using Npgsql;

namespace vm.referendum.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        NpgsqlConnection connection = new(connectionString);
        connection.Open();

        return connection;
    }
}