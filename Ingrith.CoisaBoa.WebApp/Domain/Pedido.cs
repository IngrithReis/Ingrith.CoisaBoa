using Ingrith.CoisaBoa.WebApp.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ingrith.CoisaBoa.WebApp.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal TotalPedido { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public PedidoStatusEnum Status { get; set; }
        public string Usuario { get; set; }
    }
}
