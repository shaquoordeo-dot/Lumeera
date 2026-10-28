using System;
using System.Security.Cryptography;

namespace LumeeraCare.Utils
{
    internal static class PasswordHasher
    {
        // PBKDF2 parameters
        private const int Iterations = 100_000;
        private const int SaltSize = 16; // 128-bit
        private const int HashSize = 32; // 256-bit

        public static (string Hash, string Salt) HashPassword(string password)
        {
            var saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        public static bool VerifyPassword(string password, string storedHashBase64, string storedSaltBase64)
        {
            var saltBytes = Convert.FromBase64String(storedSaltBase64);
            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            var expected = Convert.FromBase64String(storedHashBase64);
            return CryptographicOperations.FixedTimeEquals(hashBytes, expected);
        }
    }
}