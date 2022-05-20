namespace IronMacbeth.UserManagement.Contract
{
    public class LoggedInUser
    {
        public string Login { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }

        public UserRole UserRole { get; set; }
    }
}
