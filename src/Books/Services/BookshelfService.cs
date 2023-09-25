using Books.Interfaces;
using Books.Models;
using Books.Repository;

namespace Books.Services
{
    public class BookshelfService : IBookshelfService
    {
        private readonly IBookshelfRepository _bookshelfRepository;

        public BookshelfService(IBookshelfRepository bookshelfRepository)
        {
            _bookshelfRepository = bookshelfRepository;
        }


        public void Add(Bookshelf bookshelf)
        {
            _bookshelfRepository.Add(bookshelf);
        }

        public void Update(Bookshelf bookshelf)
        {

            _bookshelfRepository.Update(bookshelf);
        }

        public void Delete(Bookshelf bookshelf)
        {
            _bookshelfRepository.Delete(bookshelf.Id);
        }


        public IEnumerable<Bookshelf> GetAll()
        {
            return _bookshelfRepository.GetAll();
        }

        public Bookshelf GetById(int id)
        {
            return _bookshelfRepository.GetById(id);
        }

		public Bookshelf GetByIdUser(int idUSer)
		{
			return _bookshelfRepository.GetByIdUser(idUSer);
		}

		public IEnumerable<Book> GetBooksByIdBookshelf(int idBookshelf)
        {
            return _bookshelfRepository.GetBooksByIdBookshelf(idBookshelf);
        }

		public BookshelfBook GetBookshelfBookByIds(int idBookshelf, int idBook)
		{
		    return _bookshelfRepository.GetBookshelfBookByIds(idBookshelf, idBook);
		}

		private bool IsExistBookBookshelf(int idBookshelf, int idBook)
        {
            return _bookshelfRepository.GetBookshelfBookByIds(idBookshelf, idBook) != null;
        }

        public void AddBook(BookshelfBook model)
        {
            if (IsExistBookBookshelf(model.IdBookshelf, model.IdBook))
                throw new Exception("Такая книга уже существует на книжной полке");

            _bookshelfRepository.AddBook(model);
        }

		public void UpdateBook(BookshelfBook model)
		{
			_bookshelfRepository.UpdateBook(model);
		}

		public void DeleteBook(BookshelfBook model)
		{
			_bookshelfRepository.DeleteBook(model);
		}
	}
}
