using Books.Models;

namespace Books.ViewModels
{
	public class AddViewModel
	{
		public Book? Book { get; set; }
		
		public Author? Author { get; set; }

		public BookAuthor? BookAuthor { get; set; }
	}

	public class UpdateBookViewModel
    {
        public IEnumerable<Book>? Books { get; set; }

		public Book Book { get; set; }
    }

	public class UpdateUserViewModel
	{
		public IEnumerable<User>? Users { get; set; }

		public User User { get; set; }
	}
}
