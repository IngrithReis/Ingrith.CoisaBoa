using Microsoft.AspNetCore.Identity;

namespace Ingrith.CoisaBoa.WebApp.Domain
{
    public class Usuario : IdentityUser
    {   
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string  Complemento { get; set; }
        
        //Propriedades utilizadas para a Admin
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


    }
}
