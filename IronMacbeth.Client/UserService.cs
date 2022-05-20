using IronMacbeth.BFF.Contract;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace IronMacbeth.Client
{
    static class UserService
    {
        public static User LoggedInUser { get; private set; }

        public static bool LogIn(string login, string password)
        {
            ChannelFactory<IService> channelFactory = new ChannelFactory<IService>("IronMacbeth.BFF.Endpoint");

            channelFactory.Credentials.UserName.UserName = login;
            channelFactory.Credentials.UserName.Password = password;

            channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            var proxy = channelFactory.CreateChannel();

            ServerAdapter.Initialize(proxy);

            try
            {
                LoggedInUser = ServerAdapter.Instance.LogIn();
            }
            // Login or password is incorrect
            catch (MessageSecurityException)
            {
                ServerAdapter.ClearInstance();

                return false;
            }

            return true;
        }

        public static void LogOut()
        {
            ServerAdapter.ClearInstance();

            LoggedInUser = null;
        }
    }
}
