using Books.Interfaces;
using Books.Models;

namespace Books.Repository
{
    public class BookshelfRepository : IBookshelfRepository
    {
        private readonly AppDBContext _appDBContext;

        public BookshelfRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void Add(Bookshelf model)
        {
			_appDBContext.Bookshelf.Add(model);
			_appDBContext.SaveChanges();
		}

        public void Update(Bookshelf model)
        {
            try
            {
                _appDBContext.Bookshelf.Update(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении книжной полки");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Bookshelf bookshelf = _appDBContext.Bookshelf.Find(id);

                if (bookshelf != null)
                {
                    _appDBContext.Bookshelf.Remove(bookshelf);
                    _appDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при удалении книжной полки");
            }
        }


        public IEnumerable<Bookshelf> GetAll()
        {
            return _appDBContext.Bookshelf.ToList();
        }

        public Bookshelf GetById(int id)
        {
            return _appDBContext.Bookshelf.Find(id);
        }

		public Bookshelf GetByIdUser(int idUser)
		{
			return _appDBContext.Bookshelf.Find(idUser);
		}


		public BookshelfBook GetBookshelfBookByIds(int idBookshelf, int idBook)
        {
            return _appDBContext.BookshelfBook.FirstOrDefault(
                book => book.IdBookshelf == idBookshelf && book.IdBook == idBook);
        }

		public List<Book> GetBooksByIdBookshelf(int idBookshelf)
        {
            return _appDBContext.Book
            .Join(
                _appDBContext.BookshelfBook,
                book => book.Id,
                bookshelfBook => bookshelfBook.IdBook,
                (book, bookshelfBook) => new { Book = book, BookshelfBook = bookshelfBook }
            )
            .Where(joinedData => joinedData.BookshelfBook.IdBookshelf == idBookshelf)
            .Select(joinedData => joinedData.Book)
            .ToList();
        }


        public void AddBook(BookshelfBook model)
        {
            try
            {
                _appDBContext.BookshelfBook.Add(model);
                _appDBContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при добавлении книги на книжную полку");
            }
        }

		public void UpdateBook(BookshelfBook model)
		{
			try
			{
				_appDBContext.BookshelfBook.Update(model);
				_appDBContext.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Ошибка при обновлении книги на книжной полке");
			}
		}

		public void DeleteBook(BookshelfBook model)
		{
			try
			{
				_appDBContext.BookshelfBook.Remove(model);
				_appDBContext.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Ошибка при удалении книги с книжной полки");
			}
		}
	}
}
