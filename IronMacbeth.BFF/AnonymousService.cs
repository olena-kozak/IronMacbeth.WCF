using IronMacbeth.BFF.Contract;
using IronMacbeth.UserManagement.Contract;
using System.ServiceModel;

namespace IronMacbeth.BFF
{
    class AnonymousService : IAnonymousService
    {
        private readonly IUserManagementService _userManagementServiceClient;

        public AnonymousService()
        {
            _userManagementServiceClient = UserManagementClient.Instance;
        }

        public UserRegistrationStatus Register(string login, string password, string surname, string name, int phoneNumber)
        {
            var userRegistrationData =
                new RegisterUserRequestData
                {
                    Login = login,
                    Password = password,
                    Name = name,
                    Surname = surname,
                    PhoneNumber = phoneNumber
                };

            try
            {
                _userManagementServiceClient.RegisterUser(userRegistrationData);
            }
            catch (FaultException<UsernameTakenFault>)
            {
                return UserRegistrationStatus.UserNameAlreadyTaken;
            }

            return UserRegistrationStatus.Success;
        }

        public bool Ping()
        {
            return true;
        }
    }
}
