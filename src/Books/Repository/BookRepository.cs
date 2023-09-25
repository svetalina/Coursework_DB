using Books.Models;
using Books.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDBContext _appDBContext;

        public BookRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Book model)
        {
            try
            {
                _appDBContext.Book.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при добавлении книги");
            }
        }

        public void Update(Book model)
        {
            try
            {
                var curModel = _appDBContext.Book.FirstOrDefault(elem => elem.Id == model.Id);
                _appDBContext.Entry(curModel).CurrentValues.SetValues(model);
                
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении книги");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Book book = _appDBContext.Book.Find(id);

                if (book != null)
                {
                    _appDBContext.Book.Remove(book);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при удалении книги");
            }
        }

		public void AddBookAuthor(BookAuthor model)
        {
			try
			{
				_appDBContext.BookAuthor.Add(model);
				_appDBContext.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Ошибка при добавлении связи книга-автор");
			}
		}

        public Book GetById(int id)
        {
            return _appDBContext.Book.Find(id);
        }

		public IEnumerable<Book> GetAllGuest()
		{
			return _appDBContext.Book
								.Select(book => new Book
								{
									Name = book.Name,
									Genre = book.Genre,
									Rating = book.Rating
								})
								.ToList();
		}

		public IEnumerable<Book> GetAll()
		{
			return _appDBContext.Book.ToList();
		}

		public IEnumerable<Book> GetByNameGuest(string name)
		{
			   return _appDBContext.Book
								.Where(elem => elem.Name == name)
								.Select(book => new Book
								{
									Name = book.Name,
									Genre = book.Genre,
									Rating = book.Rating
								})
								.ToList();
		}

		public IEnumerable<Book> GetByName(string name)
        {
            return _appDBContext.Book.Where(elem => elem.Name == name).ToList();
        }


		public IEnumerable<Author> GetAuthors(int idBook)
		{
			return _appDBContext.Book
				.Where(b => b.Id == idBook)
				.Join(_appDBContext.BookAuthor, b => b.Id, ba => ba.IdBook, (b, ba) => new { Book = b, BookAuthor = ba })
				.Join(_appDBContext.Author, ab => ab.BookAuthor.IdAuthor, a => a.Id, (ab, a) => a)
				.ToList();
		}

		public IEnumerable<Book> GetByAuthor(string authorName)
		{
			return _appDBContext.Author
				.Where(a => a.Name == authorName)
				.Join(_appDBContext.BookAuthor, a => a.Id, ba => ba.IdAuthor, (a, ba) => new { Author = a, BookAuthor = ba })
				.Join(_appDBContext.Book, ab => ab.BookAuthor.IdBook, b => b.Id, (ab, b) => new { Author = ab.Author, Book = b })
				.Select(ab => ab.Book)
				.ToList();
		}

		public IEnumerable<Book> GetBySeries(string seriesName)
        {
            return _appDBContext.Series
                .Where(s => s.Name == seriesName)
                .Join(_appDBContext.BookSeries, s => s.Id, bs => bs.IdSeries, (s, bs) => new { Series = s, BookSeries = bs })
                .Join(_appDBContext.Book, sb => sb.BookSeries.IdBook, b => b.Id, (sb, b) => new { Series = sb.Series, Book = b })
                .Select(sb => sb.Book)
                .ToList();
        }

		public BookAuthor GetBookAuthorByIds(int idBook, int idAuthor)
		{
			return _appDBContext.BookAuthor.FirstOrDefault(
				ba => ba.IdBook == idBook && ba.IdAuthor == idAuthor);
		}
	}
}
