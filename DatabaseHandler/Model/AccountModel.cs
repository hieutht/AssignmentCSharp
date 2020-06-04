using System.Collections.Generic;
using DatabaseHandler.Entity;
using DatabaseHandler.Helper;
using MySql.Data.MySqlClient;

namespace DatabaseHandler.Model
{
    public class AccountModel
    {
        public void Save(Account account)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"INSERT INTO accounts (username, password, fullname, email, salt, status) " +
                $"VALUES ('{account.Username}', '{account.Password}', '{account.FullName}', '{account.Email}', '{account.Salt}', {(int) account.Status})",
                cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public void Update(string username, Account account)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"UPDATE accounts SET password = '{account.Password}', fullname = '{account.FullName}', email = '{account.Email}', status = {(int) account.Status} WHERE username = '{username}'",
                cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Delete cứng
        public void Delete(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"DELETE FROM accounts WHERE username = '{username}'",
                cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Delete mềm
        public void DeleteChangeStatus(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"UPDATE accounts set status = {AccountStatus.DEACTIVE} WHERE username = '{username}'",
                cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public List<Account> GetList()
        {
            var result = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"SELECT * FROM accounts",
                cnn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var account = new Account()
                {
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    FullName = reader.GetString("fullname"),
                    Email = reader.GetString("email"),
                    Status = (AccountStatus) reader.GetInt32("status")
                };
                result.Add(account);
            }

            cnn.Close();
            return result;
        }

        public Account GetByUsername(string username)
        {
            Account result = null;
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand(
                $"SELECT * FROM accounts where username = '{username}'",
                cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = new Account()
                {
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    FullName = reader.GetString("fullname"),
                    Email = reader.GetString("email"),
                    Salt = reader.GetString("salt"),
                    Status = (AccountStatus) reader.GetInt32("status")
                };
            }

            cnn.Close();
            return result;
        }
    }
}