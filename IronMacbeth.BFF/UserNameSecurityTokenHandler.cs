using IronMacbeth.UserManagement.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel;

namespace IronMacbeth.BFF
{
    public class UserNameSecurityTokenHandler : System.IdentityModel.Tokens.UserNameSecurityTokenHandler
    {
        public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (Configuration == null)
                throw new InvalidOperationException("No Configuration set");

            UserNameSecurityToken nameSecurityToken = token as UserNameSecurityToken;
            if (nameSecurityToken == null)
                throw new ArgumentException("SecurityToken is not a UserNameSecurityToken");

            var loggedInUser = LogIn(nameSecurityToken.UserName, nameSecurityToken.Password);

            if (loggedInUser == null)
                throw new SecurityTokenValidationException(nameSecurityToken.UserName);

            List<Claim> claimList = 
                new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, nameSecurityToken.UserName),
                    new Claim(ClaimTypes.GivenName, loggedInUser.Name),
                    new Claim(ClaimTypes.Surname, loggedInUser.Surname),
                    new Claim(ClaimTypes.MobilePhone, loggedInUser.PhoneNumber.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.Role, loggedInUser.UserRole.ToString())
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimList);

            if (Configuration.SaveBootstrapContext)
            {
                claimsIdentity.BootstrapContext = 
                    RetainPassword 
                        ? new BootstrapContext(nameSecurityToken, this)
                        : new BootstrapContext(new UserNameSecurityToken(nameSecurityToken.UserName, null), this);
            }

            return new List<ClaimsIdentity>() { new ClaimsIdentity(claimList, "Password") }.AsReadOnly();
        }

        public override bool CanValidateToken => true;

        private LoggedInUser LogIn(string userName, string password)
        {
            try
            {
                return UserManagementClient.Instance.LogIn(userName, password);
            }
            catch(FaultException<InvalidCredentialsFault>)
            {
                return null;
            }
        }
    }
}