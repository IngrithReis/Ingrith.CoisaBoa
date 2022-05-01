using System.ComponentModel.DataAnnotations;

namespace Ingrith.CoisaBoa.WebApp.Domain
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaItemId { get; set; }
        public CategoriaItem Categoria { get; set; }

        [Display(Name = "Quantidade Disponível")]
        public int QuantidadeDisponivel { get; set; }

        [Display(Name = "Controle de Estoque")]
        public bool ControlEstoque { get; set; }
        public string CaminhoImagem { get; set; }

        public void DiminuirEstoque(int quantidade)
        {
            QuantidadeDisponivel -= quantidade;
        }
        public void AumentarEstoque(int quantidade)
        {
            QuantidadeDisponivel += quantidade;
        }
    }
}
