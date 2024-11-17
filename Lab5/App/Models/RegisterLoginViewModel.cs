using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class RegisterLoginViewModel
    {
        [Required(ErrorMessage = "Ім'я користувача обов'язкове.")]
        [StringLength(50, ErrorMessage = "Ім'я користувача не повинно перевищувати 50 символів.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "ФІО обов'язкове.")]
        [StringLength(500, ErrorMessage = "ФІО не повинно перевищувати 500 символів.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Електронна адреса обов'язкова.")]
        [EmailAddress(ErrorMessage = "Некоректний формат електронної адреси.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль повинен містити від 8 до 16 символів, включаючи принаймні одну цифру, одну велику літеру та один спеціальний символ.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Підтвердження пароля обов'язкове.")]
        [Compare("Password", ErrorMessage = "Пароль і підтвердження пароля повинні збігатися.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Телефон обов'язковий.")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Некоректний формат телефону. Формат: +380XXXXXXXXX")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Електронна адреса для авторизації обов'язкова.")]
        [EmailAddress(ErrorMessage = "Некоректний формат електронної адреси.")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Пароль для авторизації обов'язковий.")]
        public string LoginPassword { get; set; }
    }
}
