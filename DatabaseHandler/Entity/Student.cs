namespace DatabaseHandler.Entity
{
    public class Student
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Status { get; set; }
        public StudentDetail Detail { get; set; }
    }
}