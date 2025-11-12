
using PrasTestJobServices.Abstract;

namespace PrasTestJobServices.Implementations
{
    public class PBKDF2PasswordHashing : IPasswordHashing
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
