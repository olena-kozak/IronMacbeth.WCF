using IronMacbeth.BFF.Contract;

namespace IronMacbeth.Client
{
    public class User
    {
        public string Login { get; set; }

        public string Surname { get; set; }

        public int PhoneNumber {get;set;}
        public string Name { get; set; }
        public UserRole UserRole { get; set; }

        public bool IsAdmin => UserRole == UserRole.Admin;
    }
}
