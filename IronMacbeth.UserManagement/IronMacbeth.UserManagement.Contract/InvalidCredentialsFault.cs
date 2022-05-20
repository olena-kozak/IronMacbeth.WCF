using System.ServiceModel;

namespace IronMacbeth.UserManagement.Contract
{
    public class InvalidCredentialsFault
    {
        public static void Throw() => throw new FaultException<InvalidCredentialsFault>(new InvalidCredentialsFault());
    }
}
