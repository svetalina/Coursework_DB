using Books.Models;
using Books.Interfaces;
using System.Security.Cryptography;

namespace Books.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private bool IsExist(User user)
        {
            return _userRepository.GetAll().FirstOrDefault(elem =>
                    elem.Login == user.Login) != null;
        }

        private bool IsNotExist(int id)
        {
            return _userRepository.GetById(id) == null;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string HashPassword(string password, byte[] salt = null)
        {
            if (salt == null || salt.Length == 0)
            {
                salt = GenerateSalt();
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                byte[] saltAndHash = new byte[48];
                Buffer.BlockCopy(salt, 0, saltAndHash, 0, 16);
                Buffer.BlockCopy(hash, 0, saltAndHash, 16, 32);
                return Convert.ToBase64String(saltAndHash);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] saltAndHash = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Buffer.BlockCopy(saltAndHash, 0, salt, 0, 16);

            string hashToCheck = HashPassword(password, salt);
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            return hashBytes.SequenceEqual(Convert.FromBase64String(hashToCheck));
        }


        public void Add(User user)
        {
            if (IsExist(user))
                throw new Exception("Пользователь с таким логином уже существует");

            _userRepository.Add(user);
        }

        public void Update(User user)
        {
            if (IsNotExist(user.Id))
                throw new Exception("Такого пользователя не существует");

            _userRepository.Update(user);
        }

        public void Delete(User user)
        {
            if (IsNotExist(user.Id))
                throw new Exception("Такого пользователя не существует");

            _userRepository.Delete(user.Id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login);
        }
    }
}