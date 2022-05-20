using System.ServiceModel;

namespace IronMacbeth.BFF.Contract
{
    [ServiceContract]
    public interface IAnonymousService
    {
        [OperationContract]
        UserRegistrationStatus Register(string login, string password, string surname, string name, int phoneNumber);

        [OperationContract]
        bool Ping();
    }
}
