using System.Diagnostics;
using Books.Models;
using Books.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Books.ViewModels;
using Books.Services;

namespace Books.Controllers
{
	public class SearchController : Controller
	{
		IAuthorService authorService;
		IBookService bookService;
		ISeriesService seriesService;

		public SearchController(IAuthorService authorService,
							  IBookService bookService,
							  ISeriesService seriesService)
		{
			this.authorService = authorService;
			this.bookService = bookService;
			this.seriesService = seriesService;
		}

		[HttpGet]
		public IActionResult SimpleSearch()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SimpleSearch(string name)
		{
			IEnumerable<Author> authors = authorService.GetByName(name);
			IEnumerable<Book> books = bookService.GetByName(name);
			IEnumerable<Series> series = seriesService.GetByName(name);

			var searchDisplayViewModel = new SearchDisplayViewModel
			{
				Author = authors,
				Book = books,
				Series = series
			};

			return View("Display", searchDisplayViewModel);
		}

		[HttpGet]
		public IActionResult BookSearch()
		{
			return View();
		}

		[HttpPost]
		public IActionResult BookSearch(BookSearchViewModel model)
		{
			var booksByAthSer = new HashSet<Book>();
			IEnumerable<Book> booksByAuthor, booksBySeries;

			if (model.AuthorName != null && model.SeriesName != null)
			{
				booksByAuthor = bookService.GetByAuthor(model.AuthorName);
				booksBySeries = bookService.GetBySeries(model.SeriesName);
				booksByAthSer.UnionWith(booksByAuthor);
				booksByAthSer.IntersectWith(booksBySeries); ;
			}
			else if (model.AuthorName != null)
			{
				booksByAuthor = bookService.GetByAuthor(model.AuthorName);
				booksByAthSer.UnionWith(booksByAuthor);
			}
			else if (model.SeriesName != null)
			{
				booksBySeries = bookService.GetBySeries(model.SeriesName);
				booksByAthSer.UnionWith(booksBySeries);
			}

			IEnumerable<Book> books = booksByAthSer;

			books = bookService.GetByParameters(books, model.Book,
												model.YearFlag, model.RatingFlag);

			var searchDisplayViewModel = new SearchDisplayViewModel
			{
				Book = books,
				AuthorName = model.AuthorName,
				SeriesName = model.SeriesName
			};

			return View("Display", searchDisplayViewModel);
		}

		[HttpGet]
		public IActionResult AuthorSearch()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AuthorSearch(AuthorSearchViewModel model)
		{
			IEnumerable<Author> authors = authorService.GetByParameters(model.Author,
																		model.YearBirthFlag,
																		model.YearDeathFlag);

			var searchDisplayViewModel = new SearchDisplayViewModel
			{
				Author = authors
			};

			return View("Display", searchDisplayViewModel);
		}

		[HttpGet]
		public IActionResult SeriesSearch()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SeriesSearch(SeriesSearchViewModel model)
		{
			IEnumerable<Series> series = seriesService.GetByParameters(model.Series,
																		model.YearFlag,
																		model.RatingFlag);

			var searchDisplayViewModel = new SearchDisplayViewModel
			{
				Series = series
			};

			return View("Display", searchDisplayViewModel);
		}
	}
}