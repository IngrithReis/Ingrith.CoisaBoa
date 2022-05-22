namespace Ingrith.CoisaBoa.WebApp.Domain
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ItemId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }

        public Item Item { get; set; }
        public Pedido Pedido { get; set; }

    }
}
