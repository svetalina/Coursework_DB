using Books.Models;

namespace Books.Interfaces
{
    public interface IUserService : IService<User>
    {
        string HashPassword(string password, byte[]? salt = null);
        bool VerifyPassword(string password, string hashedPassword);

        User GetByLogin(string login);
    }
}
