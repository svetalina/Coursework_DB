using Books.Interfaces;
using Books.Models;
using Books.Services;
using Books.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace Books.Controllers
{
	public class BookshelfController : Controller
	{
		IBookService bookService;
		IBookshelfService bookshelfService;
		IUserService userService;

		public BookshelfController(IBookService bookService,
								   IBookshelfService bookshelfService,
								   IUserService userService)
		{
			this.bookService = bookService;
			this.bookshelfService = bookshelfService;
			this.userService = userService;
		}

		public IActionResult AddBook(int idBook)
		{
			if (ModelState.IsValid)
			{
				try
				{
					string login = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
					int idUser = userService.GetByLogin(login).Id;

					var bookshelfbook = new BookshelfBook
					{
						IdBookshelf = bookshelfService.GetByIdUser(idUser).Id,
						IdBook = idBook,
						IsRead = false
					};

					bookshelfService.AddBook(bookshelfbook);
					return Json(new { success = true });
				}
				catch (Exception ex)
				{
					return Json(new { success = false, message = ex.Message });
				}
			}
			else
			{
				return Json(new { success = false, message = "Некорректные данные" });
			}			
		}

		public IActionResult DeleteBook(int idBookshelf, int idBook)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var bookshelfbook = new BookshelfBook
					{
						IdBookshelf = idBookshelf,
						IdBook = idBook
					};

					bookshelfService.DeleteBook(bookshelfbook);
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return RedirectToAction("Display", "Bookshelf");
		}

		public IActionResult ReadBook(int idBookshelf, int idBook)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var bookshelfbook = new BookshelfBook
					{
						IdBookshelf = idBookshelf,
						IdBook = idBook,
						IsRead = true
					};

					bookshelfService.UpdateBook(bookshelfbook);
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return RedirectToAction("Display", "Bookshelf");
		}

		public async Task<IActionResult> Display()
		{
			string login = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			int idUser = userService.GetByLogin(login).Id;
			
			Bookshelf bookshelf = bookshelfService.GetByIdUser(idUser);

			IEnumerable<Book> books = bookshelfService.GetBooksByIdBookshelf(bookshelf.Id);

			var booksViewModels = new List<BooksBookshelfDisplayViewModel>();

			foreach (var book in books)
			{
				IEnumerable<Author> authors = bookService.GetAuthors(book.Id);
				var bookViewModel = new BooksBookshelfDisplayViewModel
				{
					Book = book,
					IsRead = bookshelfService.GetBookshelfBookByIds(bookshelf.Id, book.Id).IsRead,
					Authors = authors
				};
				booksViewModels.Add(bookViewModel);
			}

			var bookshelfDisplayViewModel = new BookshelfDisplayViewModel
			{ 
				Bookshelf = bookshelf,
				Books = booksViewModels
			};

			return View(bookshelfDisplayViewModel);
		}
	}
}
