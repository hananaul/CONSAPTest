using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CONSAPTest.DatabaseSettings
{
    public class MySqlDatabase : IDisposable
    {
        public MySqlConnection _connection;

        public MySqlDatabase(string connection)
        {
            _connection = new MySqlConnection(connection);
            _connection.Open();
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connection.ConnectionString);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
