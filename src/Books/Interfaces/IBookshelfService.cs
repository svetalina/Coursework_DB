using Books.Models;

namespace Books.Interfaces
{
    public interface IBookshelfService : IService<Bookshelf>
    {
        Bookshelf GetByIdUser(int idUser);

		void AddBook(BookshelfBook model);
		void DeleteBook(BookshelfBook model);
		void UpdateBook(BookshelfBook model);

		IEnumerable<Book> GetBooksByIdBookshelf(int idBookshelf);

		BookshelfBook GetBookshelfBookByIds(int idBookshelf, int idBook);

	}
}