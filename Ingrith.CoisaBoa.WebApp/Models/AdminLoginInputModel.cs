using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ingrith.CoisaBoa.WebApp.Models
{
    public class AdminLoginInputModel
    {
        [PersonalData]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        

        
    }
}
