using Books.Models;

namespace Books.Interfaces
{
    public interface IBookshelfRepository : IRepository<Bookshelf>
    {
        Bookshelf GetByIdUser(int idUSer);

		void AddBook(BookshelfBook model);
		void DeleteBook(BookshelfBook model);
		void UpdateBook(BookshelfBook model);

		BookshelfBook GetBookshelfBookByIds(int idBookshelf, int idBook);
        List<Book> GetBooksByIdBookshelf(int idBookshelf);
    }
}
