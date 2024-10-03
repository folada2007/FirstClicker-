using System.ComponentModel.DataAnnotations;

namespace ClickME.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MinLength(8,ErrorMessage = "Пароль не менее 8 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Email { get; set; }
    }
}
