using Books.Models;
using Books.ViewModels;
using Books.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Books.Controllers
{
	public class AccountController : Controller
	{
		IConfiguration configuration;
		IUserService userService;
		IBookshelfService bookshelfService;

		public AccountController(IConfiguration configuration,
								 IUserService userService,
								 IBookshelfService bookshelfService)
		{
			this.configuration = configuration;
			this.userService = userService;
			this.bookshelfService = bookshelfService;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					User user = new User
					{
						Login = model.Login,
						Password = userService.HashPassword(model.Password),
						Permission = "user"
					};
					userService.Add(user);

					Bookshelf bookshelf = new Bookshelf
					{
						IdUser = userService.GetByLogin(user.Login).Id,
						Number = 0,
						Rating = 0
					};
					bookshelfService.Add(bookshelf);

					await Authenticate(user);
					ChangeConnection(user.Permission);

					return RedirectToAction("SimpleSearch", "Search");
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return View(model);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = userService.GetByLogin(model.Login);

				if (user == null)
					TempData["ErrorMessage"] = "Некорректный логин";
				else if (!userService.VerifyPassword(model.Password, user.Password))
					TempData["ErrorMessage"] = "Некорректный пароль";
				else
				{
					await Authenticate(user);
					ChangeConnection(user.Permission);

					return RedirectToAction("SimpleSearch", "Search");
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return View(model);
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string login = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				User user = userService.GetByLogin(login);

				if (userService.VerifyPassword(model.OldPassword, user.Password))
				{
					user.Password = userService.HashPassword(model.NewPassword);
					userService.Update(user);

					return RedirectToAction("SimpleSearch", "Search");
				}
				else
					TempData["ErrorMessage"] = "Некорректный пароль";
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return View(model);
		}

		public async Task<IActionResult> DeleteUser()
		{
			if (ModelState.IsValid)
			{
				string login = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				User user = userService.GetByLogin(login);

				try
				{
					userService.Delete(user);
					await Logout();
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
				TempData["ErrorMessage"] = "Некорректные данные";

			return RedirectToAction("SimpleSearch", "Search");
		}

		private async Task Authenticate(User user)
		{
			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Login));
			claims.Add(new Claim(ClaimTypes.Role, user.Permission));

			ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			AuthenticationProperties properties = new AuthenticationProperties()
			{ AllowRefresh = false };

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(identity), properties);

		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			ChangeConnection("guest");

			return RedirectToAction("SimpleSearch", "Search");
		}

		private void ChangeConnection(string permission)
		{
			if (permission == "user")
				configuration["DatabaseConnection"] = configuration.GetConnectionString("userConnection");
			else if (permission == "admin")
				configuration["DatabaseConnection"] = configuration.GetConnectionString("AdminConnection");
			else
				configuration["DatabaseConnection"] = configuration.GetConnectionString("guestConnection");
		}
	}
}
