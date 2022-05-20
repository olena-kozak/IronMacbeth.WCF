using IronMacbeth.BFF.Contract;
using System;

namespace IronMacbeth.Client
{
    class AnonymousServerAdapter
    {
        private static AnonymousServerAdapter _instance;
        public static AnonymousServerAdapter Instance => _instance ?? throw new Exception($"{nameof(ServerAdapter)} was not initialized. Use '{nameof(Initialize)}' method.");

        public static void Initialize(IAnonymousService proxy)
        {
            if (_instance != null)
            {
                throw new Exception($"{nameof(ServerAdapter.Instance)} was already initialized.");
            }

            _instance = new AnonymousServerAdapter(proxy);
        }

        public static void ClearInstance()
        {
            _instance = null;
        }

        private IAnonymousService _proxy;

        private AnonymousServerAdapter(IAnonymousService proxy)
        {
            _proxy = proxy;
        }

        public UserRegistrationStatus Register(string login, string password, string name, string surname, int phoneNumber)
        {
            return _proxy.Register(login, password, surname,name,phoneNumber );
        }

        public bool Ping()
        {
            return _proxy.Ping();
        }
    }
}
