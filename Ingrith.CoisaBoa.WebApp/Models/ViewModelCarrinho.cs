using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ingrith.CoisaBoa.WebApp.Models
{
    public class ViewModelCarrinho
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal TotalPedido { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public Item Item { get; set; }
        public int Quantidade { get; set; }

        public decimal Preco { get; set; }
        public PedidoStatusEnum Status { get; set; }
        public string Usuario { get; set; }
    }
}
