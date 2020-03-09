using System.ComponentModel.DataAnnotations;

namespace SocialNet.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не введено имя")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Не введена фамилия")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage ="Не указан Email")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}