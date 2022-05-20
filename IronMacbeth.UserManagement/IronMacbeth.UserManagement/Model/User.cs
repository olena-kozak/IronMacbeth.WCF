using IronMacbeth.UserManagement.Contract;

namespace IronMacbeth.UserManagement.Model
{
    class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }

        public UserRole UserRole { get; set; }
    }
}
