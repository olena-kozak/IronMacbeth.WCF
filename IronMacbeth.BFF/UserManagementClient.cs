using IronMacbeth.UserManagement.Contract;
using System.ServiceModel;

namespace IronMacbeth.BFF
{
    class UserManagementClient
    {
        public static IUserManagementService Instance { get; }

        static UserManagementClient()
        {
            Instance = new ChannelFactory<IUserManagementService>("IronMacbeth.UserManagementEndpoint").CreateChannel();
        }
    }
}
