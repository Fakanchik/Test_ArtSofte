using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Поле ФИО не заполнено.")]
        [StringLength(250)]
        [Display(Name = "ФИО")]
        public string FIO { get; set; }
        [Required(ErrorMessage = "Поле телефона не заполнено.")]
        [StringLength(11)]
        [Display(Name = "Телефон")]
        [RegularExpression(@"7[0-9]{10}", ErrorMessage = "Номер телефона должен начинаться с 7")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Поле электронной почты не заполнено.")]
        [StringLength(150)]
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле пароль не заполнено.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Минимальная длина пароля 6 символов.")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string PasswordConfirm { get; set; }
    }
}
