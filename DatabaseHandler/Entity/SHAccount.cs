using System;

namespace DatabaseHandler.Entity
{
    public class SHAccount
    {
        public string AccountNumber { get; set; }
        public string IdentityNumber { get; set; }
        public double Balance { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public SHAccountStatus Status { get; set; }
    }

    public enum SHAccountStatus
    {
        ACTIVE = 1,
        DEACTIVE = 0,
        LOCK = 2
    }
}