namespace PrasTestJobServices.Abstract
{
    public interface IPasswordHashing
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
