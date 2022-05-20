namespace IronMacbeth.BFF.Contract
{
    public class User
    {
        public string Login { get; set; }
        public string Surname { get; set; }

        public int PhoneNumber { get; set; }
        public string Name { get; set; }

        public UserRole UserRole { get; set; }
    }
}
