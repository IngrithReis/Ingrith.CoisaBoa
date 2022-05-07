using Ingrith.CoisaBoa.WebApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ingrith.CoisaBoa.WebApp.Domain
{
    public class Pedido
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime DataVenda { get; set; }
        public decimal TotalPedido { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public PedidoStatusEnum Status { get; set; }
        public string Usuario { get; set; }

        [Display(Name = "Observações do Pedido:")]
        public string Observacao { get; set; }

        public PagamentoEnum Pagamento { get; set; }

        //taxa de entrega fixada para ser posteriormente tratada no desenvolver do projeto
        public decimal TaxaEntrega { get; set; }

        

    }
}
