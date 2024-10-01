using System.ComponentModel.DataAnnotations;

namespace ClickME.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(8,ErrorMessage = "Длина пароля не менее 8 символов")]
        public string Password { get; set; }
    }
}
