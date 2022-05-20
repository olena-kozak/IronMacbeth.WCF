using IronMacbeth.UserManagement.Contract;
using IronMacbeth.UserManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace IronMacbeth.UserManagement
{
    class UserManagementService : IUserManagementService
    {
        public LoggedInUser LogIn(string login, string password)
        {
            User user;

            using (var dbContext = new DbContext())
            {
                user = dbContext.Users.Where(x => x.Login == login).SingleOrDefault();
            }

            if (user == null)
            {
                InvalidCredentialsFault.Throw();
            }

            if (!SecurePasswordHasher.Verify(password, user.PasswordHash))
            {
                InvalidCredentialsFault.Throw();
            }

            var loggedInUser = MapInternalToContract(user);

            return loggedInUser;
        }

        public void RegisterUser(RegisterUserRequestData userData)
        {
            var passwordHash = SecurePasswordHasher.Hash(userData.Password);

            var userToCreate =
                new User
                {
                    Login = userData.Login,
                    PasswordHash = passwordHash,
                    Name = userData.Name,
                    Surname = userData.Surname,
                    PhoneNumber = userData.PhoneNumber,
                    UserRole = UserRole.User
                };

            using (var dbContext = new DbContext())
            {
                dbContext.Users.Add(userToCreate);

                try
                {
                    dbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && sqlException.Number == 2627) // 2627 = unique (primary key) constraint violation
                {
                    UsernameTakenFault.Throw();
                }
            }
        }

        private LoggedInUser MapInternalToContract(User user)
        {
            return new LoggedInUser
            {
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                UserRole = user.UserRole
            };
        }
    }
}
