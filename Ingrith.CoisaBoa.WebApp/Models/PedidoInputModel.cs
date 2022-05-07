using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ingrith.CoisaBoa.WebApp.Models
{
    public class PedidoInputModel
    {
        public int Id { get; set; }

       

        public string Observacao { get; set; }

        public PagamentoEnum Pagamento { get; set; }

        //taxa de entrega fixada para ser posteriormente tratada no desenvolver do projeto
        
    }
}
