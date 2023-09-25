using Books.Models;
using Books.ViewModels;

namespace Books.Interfaces
{
    public interface IBookService : IService<Book>
    {
		void AddBookAuthor(BookAuthor model);

		IEnumerable<Book> GetByName(string name);
        
        IEnumerable<Author> GetAuthors(int idBook);

		IEnumerable<Book> GetByAuthor(string authorName);
		IEnumerable<Book> GetBySeries(string seriesName);

		IEnumerable<Book> GetByParameters(IEnumerable<Book> bookSet,
												Book parameters,
												SearchFlag yearFlag,
												SearchFlag ratingFlag);
	}
}
