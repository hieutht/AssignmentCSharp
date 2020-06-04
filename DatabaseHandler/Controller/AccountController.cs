using System;
using DatabaseHandler.Entity;
using DatabaseHandler.Helper;
using DatabaseHandler.Model;

namespace DatabaseHandler.Controller
{
    public class AccountController
    {
        AccountModel model = new AccountModel();

        public void Register()
        {
            Console.WriteLine("Please enter your username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            var password = Console.ReadLine();
            Console.WriteLine("Please enter your fullname: ");
            var fullname = Console.ReadLine();
            Console.WriteLine("Please enter your email: ");
            var email = Console.ReadLine();
            // validate user data.
            var salt = PasswordHelper.GenerateSalt();
            var encryptPassword = PasswordHelper.EncryptString(password + salt);
            var account = new Account()
            {
                Username = username,
                Password = encryptPassword,
                FullName = fullname,
                Email = email,
                Salt = salt,
                Status = AccountStatus.ACTIVE
            };
            model.Save(account);
            Console.WriteLine("Create account success!");
        }

        public bool Login()
        {
            Console.WriteLine("Please enter your username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            var password = Console.ReadLine();
            var existingAccount = model.GetByUsername(username);
            return existingAccount != null && existingAccount.Status == AccountStatus.ACTIVE
                                           && PasswordHelper.ComparePassword(password, existingAccount.Password,
                                               existingAccount.Salt);
        }
    }
}