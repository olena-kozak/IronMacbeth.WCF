using System.ServiceModel;

namespace IronMacbeth.UserManagement.Contract
{
    [ServiceContract]
    public interface IUserManagementService
    {
        [OperationContract]
        [FaultContract(typeof(InvalidCredentialsFault))]
        LoggedInUser LogIn(string login, string password);

        [OperationContract]
        [FaultContract(typeof(UsernameTakenFault))]
        void RegisterUser(RegisterUserRequestData userData);
    }
}
