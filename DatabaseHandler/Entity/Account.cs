namespace DatabaseHandler.Entity
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Salt { get; set; }

        public AccountStatus Status { get; set; }

        public override string ToString()
        {
            return $"Username: {Username}, Password: {Password}";
        }
    }

    public enum AccountStatus
    {
        ACTIVE = 1, DEACTIVE = 0
    }
}