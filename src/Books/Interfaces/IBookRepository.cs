using Books.Models;

namespace Books.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
		public void AddBookAuthor(BookAuthor model);

		IEnumerable<Book> GetAllGuest();

		IEnumerable<Book> GetByNameGuest(string name);
		IEnumerable<Book> GetByName(string name);

		IEnumerable<Book> GetByAuthor(string authorName);
		
        IEnumerable<Book> GetBySeries(string seriesName);

		IEnumerable<Author> GetAuthors(int idBook);

		BookAuthor GetBookAuthorByIds(int idBook, int idAuthor);
	}
}