using Books.Models;

namespace Books.ViewModels
{
    public class SearchDisplayViewModel
	{
        public IEnumerable<Book>? Book { get; set; }

		public string AuthorName { get; set; }

		public string SeriesName { get; set; }

		public IEnumerable<Author>? Author { get; set; }

        public IEnumerable<Series>? Series { get; set; }

	}

    public enum SearchFlag
    {
		Equal,
		More,
        Less
    }

	public class AuthorSearchViewModel
	{
		public Author Author { get; set; }

		public SearchFlag YearBirthFlag { get; set; }

		public SearchFlag YearDeathFlag { get; set; }
	}

	public class BookSearchViewModel
    {
        public Book Book { get; set; }

		public SearchFlag YearFlag { get; set; }

		public SearchFlag RatingFlag { get; set; }

		public string AuthorName { get; set; }

        public string SeriesName { get; set; }
    }

	public class SeriesSearchViewModel
	{
		public Series Series { get; set; }

		public SearchFlag YearFlag { get; set; }
		
		public SearchFlag RatingFlag { get; set; }
	}
}