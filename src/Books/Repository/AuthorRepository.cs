using Books.Models;
using Books.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDBContext _appDBContext;

        public AuthorRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Author model)
        {
            try
            {
                _appDBContext.Author.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при добавлении автора");
            }
        }

        public void Update(Author model)
        {
            try
            {
                _appDBContext.Author.Update(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении автора");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Author author = _appDBContext.Author.Find(id);

                if (author != null)
                {
                    _appDBContext.Author.Remove(author);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при удалении автора");
            }
        }

		public IEnumerable<Author> GetAllGuest()
		{
			return _appDBContext.Author
								.Select(author => new Author
								{
									Name = author.Name,
									Genre = author.Genre,
									Country = author.Country
								})
								.ToList();
		}

		public IEnumerable<Author> GetAll()
		{
			return _appDBContext.Author.ToList();
		}


		public Author GetById(int id)
        {
            return _appDBContext.Author.Find(id);
        }

		public IEnumerable<Author> GetByNameGuest(string name)
		{
			return _appDBContext.Author
								.Where(elem => elem.Name == name)
								.Select(author => new Author
								{
									Name = author.Name,
									Genre = author.Genre,
									Country = author.Country
								})
								.ToList();
		}

		public IEnumerable<Author> GetByName(string name)
        {
            return _appDBContext.Author.Where(elem => elem.Name == name).ToList();
        }


		public IEnumerable<Author> GetByYearBirth(int yearBirth)
        {
            return _appDBContext.Author.Where(elem => elem.YearBirth == yearBirth).ToList();
        }

        public IEnumerable<Author> GetByYearDeath(int yearDeath)
        {
            return _appDBContext.Author.Where(elem => elem.YearDeath == yearDeath).ToList();
        }

        public IEnumerable<Author> GetByCountry(string country)
        {
            return _appDBContext.Author.Where(elem => elem.Country == country).ToList();
        }

        public IEnumerable<Author> GetByGenre(string genre)
        {
            return _appDBContext.Author.Where(elem => elem.Genre == genre).ToList();
        }
    }
}
