using System.ComponentModel.DataAnnotations;

namespace testTask.Models
{
    public class RegistrationUserModel
    {

        [Required(ErrorMessage = "Не указан имя")]
        [MaxLength(25)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [MaxLength(25)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан Login")]
        [MaxLength(30)]
        [MinLength(5)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Введен невалидный Email")]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не введен пароль повторно")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set;}
    }
}
