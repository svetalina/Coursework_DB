using Books.Models;

namespace Books.ViewModels
{

	public class BookshelfDisplayViewModel
	{
		public Bookshelf Bookshelf { get; set; }

		public List<BooksBookshelfDisplayViewModel> Books { get; set; }
	}

	public class BooksBookshelfDisplayViewModel
	{
		public Book Book { get; set; }

		public bool IsRead { get; set; }

		public IEnumerable<Author> Authors { get; set; }
	}
}
