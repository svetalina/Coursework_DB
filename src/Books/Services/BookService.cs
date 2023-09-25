using Books.Models;
using Books.Interfaces;
using Books.ViewModels;
using Books.Repository;

namespace Books.Services
{
    public class BookService : IBookService
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IBookRepository _bookRepository;

        public BookService(IHttpContextAccessor httpContextAccessor,
						   IBookRepository bookRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookRepository = bookRepository;
        }


        private bool IsExist(Book book)
        {
            return _bookRepository.GetAll().FirstOrDefault(elem =>
                    elem.Name == book.Name) != null;
        }

        private bool IsNotExist(int id)
        {
            return _bookRepository.GetById(id) == null;
        }

		private bool IsExistBookAuthor(int idBook, int idAuthor)
		{
			return _bookRepository.GetBookAuthorByIds(idBook, idAuthor) != null;
		}

		public void Add(Book book)
        {
            if (IsExist(book))
                throw new Exception("Такая книга уже существует");

            _bookRepository.Add(book);
        }

        public void Update(Book book)
        {
            if (IsNotExist(book.Id))
                throw new Exception("Такой книги не существует");

            _bookRepository.Update(book);
        }   

        public void Delete(Book book)
        {
            if (IsNotExist(book.Id))
                throw new Exception("Такой книги не существует");

            _bookRepository.Delete(book.Id);
        }

		public void AddBookAuthor(BookAuthor model)
		{
			if (IsExistBookAuthor(model.IdBook, model.IdAuthor))
				throw new Exception("Такая связь книга-автор уже существует");

			_bookRepository.AddBookAuthor(model);
		}

		
        public IEnumerable<Book> GetAll()
        {
			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				return _bookRepository.GetAllGuest();

			return _bookRepository.GetAll();
		}

        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }


        public IEnumerable<Book> GetByName(string name)
        {
			var curUser = _httpContextAccessor.HttpContext.User;

			if (!curUser.Identity.IsAuthenticated)
				return _bookRepository.GetByNameGuest(name);
			
            return _bookRepository.GetByName(name);
        }

		public IEnumerable<Book> GetByAuthor(string authorName)
		{
			return _bookRepository.GetByAuthor(authorName);
		}

		public IEnumerable<Book> GetBySeries(string seriesName)
        {
            return _bookRepository.GetBySeries(seriesName);
        }


		public IEnumerable<Author> GetAuthors(int idBook)
		{
			return _bookRepository.GetAuthors(idBook);
		}


		public IEnumerable<Book> GetByParameters(IEnumerable<Book> books,
                                                Book parameters, 
                                                SearchFlag yearFlag, 
                                                SearchFlag ratingFlag)
        {
			if (!books.Any())
			{
				var curUser = _httpContextAccessor.HttpContext.User;

				if (!curUser.Identity.IsAuthenticated)
					books = _bookRepository.GetAllGuest();
				else
					books = _bookRepository.GetAll();
			}

            if (books.Count() != 0 && parameters.Name != null)
				books = books.Where(elem => elem.Name == parameters.Name);

            if (books.Count() != 0 && parameters.Genre != null)
				books = books.Where(elem => elem.Genre == parameters.Genre);

            if (books.Count() != 0 && parameters.Language != null)
				books = books.Where(elem => elem.Language == parameters.Language);

			if (books.Count() != 0 && parameters.Year != 0)
			{
				if (yearFlag == SearchFlag.Equal)
					books = books.Where(elem => elem.Year == parameters.Year);
				if (yearFlag == SearchFlag.More)
					books = books.Where(elem => elem.Year >= parameters.Year);
				if (yearFlag == SearchFlag.Less)
					books = books.Where(elem => elem.Year <= parameters.Year);
			}

			if (books.Count() != 0 && parameters.Rating != 0)
			{
				if (ratingFlag == SearchFlag.Equal)
					books = books.Where(elem => elem.Rating == parameters.Rating);
				if (ratingFlag == SearchFlag.More)
					books = books.Where(elem => elem.Rating >= parameters.Rating);
				if (ratingFlag == SearchFlag.Less)
					books = books.Where(elem => elem.Rating <= parameters.Rating);
			}

			return books;
        }
	}
}
