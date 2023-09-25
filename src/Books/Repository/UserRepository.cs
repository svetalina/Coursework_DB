using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _appDBContext;

        public UserRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(User model)
        {
            try
            {
                _appDBContext.User.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при добавлении пользователя");
            }
        }

        public void Update(User model)
        {
            try
            {
				var user = _appDBContext.User.FirstOrDefault(elem => elem.Id == model.Id);
				
                _appDBContext.Entry(user).CurrentValues.SetValues(model);
				_appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении пользователя");
            }
        }

        public void Delete(int id)
        {
            try
            {
                User user = _appDBContext.User.Find(id);

                if (user != null)
                {
                    _appDBContext.User.Remove(user);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при удалении пользователя");
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _appDBContext.User.ToList();
        }

        public User GetById(int id)
        {
            return _appDBContext.User.Find(id);
        }

        public User GetByLogin(string login)
        {
            return _appDBContext.User.FirstOrDefault(elem => elem.Login == login);
        }
    }
}