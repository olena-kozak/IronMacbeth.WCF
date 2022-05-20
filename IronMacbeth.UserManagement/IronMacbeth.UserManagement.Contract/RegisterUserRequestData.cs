namespace IronMacbeth.UserManagement.Contract
{
    public class RegisterUserRequestData
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
    }
}
