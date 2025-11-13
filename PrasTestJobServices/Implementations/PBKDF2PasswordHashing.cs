using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PrasTestJobServices.Abstract;

namespace PrasTestJobServices.Implementations
{
    public class PBKDF2PasswordHashing : IPasswordHashing
    {
        const int _saltSize = 16;
        const int _hashSize = 32;
        const int _iterations = 1000;


        private byte[] GenerateSalt()
        {
            var salt = new byte[_saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string Pbkdf2(string password, byte[] salt)
        {
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: _iterations,
                numBytesRequested: _hashSize
                );
            var hashAndSalt = new byte[_saltSize + _hashSize];
            Array.Copy(salt, 0, hashAndSalt, 0, _saltSize);
            Array.Copy(hash, 0, hashAndSalt, _saltSize, _hashSize);
            return Convert.ToBase64String(hashAndSalt);
        }

        public string HashPassword(string password)
        {
            var salt = GenerateSalt();
            return Pbkdf2(password, salt);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var salt = new byte[_saltSize];
            var hashAndSalt = Convert.FromBase64String(passwordHash);
            Array.Copy(hashAndSalt, 0, salt, 0, _saltSize);
            return Pbkdf2(password, salt) == passwordHash;
        }
    }
}
