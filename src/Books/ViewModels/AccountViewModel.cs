using System.ComponentModel.DataAnnotations;

namespace Books.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

	public class ChangePasswordViewModel
	{
		[Required(ErrorMessage = "Не указан старый пароль")]
		public string OldPassword { get; set; }

		[Required(ErrorMessage = "Не указан пароль")]
		public string NewPassword { get; set; }

		[Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
		public string ConfirmNewPassword { get; set; }
	}

}