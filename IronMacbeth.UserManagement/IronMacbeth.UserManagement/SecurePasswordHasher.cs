using System;
using System.Linq;
using System.Security.Cryptography;

namespace IronMacbeth.UserManagement
{
    public static class SecurePasswordHasher
    {
        private const int SaltSize = 16;

        private const int HashSize = 20;

        private const int Iterations = 10000;

        public static string Hash(string password)
        {
            // Create salt
            byte[] salt = new byte[SaltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return base64Hash;
        }

        public static bool Verify(string password, string hashedPassword)
        {
            // Get hash bytes
            var hashBytes = Convert.FromBase64String(hashedPassword);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            var isMatch = hashBytes.Skip(SaltSize).SequenceEqual(hash);

            return isMatch;
        }
    }
}
