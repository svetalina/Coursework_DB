using Books.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Books.Models;
using Books.ViewModels;
using Books.Services;

namespace Books.Controllers
{
	public class AdminController : Controller
	{
		IAuthorService authorService;
		IBookService bookService;
		IUserService userService;

		public AdminController(IAuthorService authorService,
							   IBookService bookService, 
							   IUserService userService)
		{
			this.authorService = authorService;
			this.bookService = bookService;
			this.userService = userService;
		}

		[HttpGet]
		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Add(AddViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Book != null)
				{
					try
					{
						bookService.Add(model.Book);
					}
					catch (Exception ex)
					{
						TempData["ErrorMessage"] = ex.Message;
					}
				}

				if (model.Author != null)
				{
					try
					{
						authorService.Add(model.Author);
					}
					catch (Exception ex)
					{
						TempData["ErrorMessage"] = ex.Message;
					}
				}

				if (model.BookAuthor != null)
				{
					try
					{
						bookService.AddBookAuthor(model.BookAuthor);
					}
					catch (Exception ex)
					{
						TempData["ErrorMessage"] = ex.Message;
					}
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return RedirectToAction("Add", "Admin");
		}

		[HttpGet]
		public ActionResult UpdateBook()
		{
			var updateBookViewModel = new UpdateBookViewModel
			{
				Books = bookService.GetAll()
			};

			return View(updateBookViewModel);
		}

		[HttpPost]
		public ActionResult UpdateBook(UpdateBookViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					bookService.Update(model.Book);
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return RedirectToAction("UpdateBook", "Admin");
		}

		[HttpGet]
		public ActionResult UpdateUser()
		{
			var updateUserViewModel = new UpdateUserViewModel
			{
				Users = userService.GetAll()
			};

			return View(updateUserViewModel);
		}

		[HttpPost]
		public ActionResult UpdateUser(UpdateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					userService.Update(model.User);
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return RedirectToAction("UpdateUser", "Admin");
		}

	}
}
