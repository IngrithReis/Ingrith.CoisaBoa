using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ingrith.CoisaBoa.WebApp.Models
{
    public class AdmRegisterInputModel
    {
        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name = "Como quer ser chamado(a)?")]
        public string Nome { get; set; }

        [PersonalData]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        public string ConfirmPassword { get; set; }

    }
}
