using System.ServiceModel;

namespace IronMacbeth.UserManagement.Contract
{
    public class UsernameTakenFault
    {
        public static void Throw() => throw new FaultException<UsernameTakenFault>(new UsernameTakenFault());
    }
}
