using System.ComponentModel.DataAnnotations;

namespace Ingrith.CoisaBoa.WebApp.Models
{
    public class UsuarioLoginInputModel
    {
        [Required]
        [DataType(DataType.Text)]
        
        public string Telefone { get; set; }

        
    }
}
