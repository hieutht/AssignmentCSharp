using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseHandler.Helper
{
    public class ConnectionHelper
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "spring-hero-bank-t1908m";
        private const string DatabaseUsername = "root";
        private const string DatabasePassword = "abcD1234";

        private static MySqlConnection _connection; // null

        public static MySqlConnection GetConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Broken)
            {
                return _connection;
            }
            Console.WriteLine("Connect to database...");
            _connection =
                new MySqlConnection(
                    $"SERVER={DatabaseServer};DATABASE={DatabaseName};UID={DatabaseUsername};PASSWORD={DatabasePassword}");
            Console.WriteLine("...success!");
            return _connection;
        }
    }
}