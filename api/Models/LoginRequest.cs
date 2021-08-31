using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Введите номер телефона.")]
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        [RegularExpression(@"7[0-9]{10}", ErrorMessage = "Номер телефона должен начинаться с 7")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Необходимо ввести пароль.")]
        [StringLength(20)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
