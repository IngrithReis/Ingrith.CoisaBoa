using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingrith.CoisaBoa.WebApp.Models
{
    [AllowAnonymous]
    public class UsuarioInputRegisterModel 
    {   
        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(11)")]
        [DataType(DataType.Text)]
        [Display(Name = "Telefone com DDD")]
        public string Telefone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }


    }
}
