using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;

namespace Ingrith.CoisaBoa.WebApp.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Item> Item { get; set; }
        public DbSet<CategoriaItem> CategoriaItem { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }
        public DbSet<PedidoStatus> PedidoStatus { get; set; }

        public DbSet<BairrosAtendidos> BairrosAtendidos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoriaItem>(modelBuilder =>
            {
                modelBuilder.HasData(
                    new CategoriaItem {
                        Id = 1,
                        Nome = "Porções"
                    }
                );
            });

            builder.Entity<Item>(modelBuilder =>
            {
                modelBuilder.HasData(
                    new Item {
                        Id = 1,
                        CategoriaItemId = 1,
                        Nome = "Batata frita",
                        Preco = 10.5m,
                        ControlEstoque = true,
                        QuantidadeDisponivel = 1000,
                        CaminhoImagem = "batata.jpg"
                    }
                );
            });

            builder.Entity<BairrosAtendidos>(modelBuilder =>
            {
                modelBuilder.HasData(
                    new BairrosAtendidos
                    {
                        Id = 1,
                        Nome = "Vila Planalto",
                    },
                    new BairrosAtendidos
                    {
                        Id = 2,
                        Nome = "Tangamandápio",
                    },
                    new BairrosAtendidos
                    {
                        Id = 3,
                        Nome = "Gopoúva",
                    },
                     new BairrosAtendidos
                     {
                         Id = 4,
                         Nome = "Vila Sésamo",
                     }
                );
            });

            builder.Entity<PedidoStatus>(modelBuilder =>
            {
                modelBuilder.Property(x => x.Id).ValueGeneratedNever();

                modelBuilder.HasData(
                    new PedidoStatus { Id = (int)PedidoStatusEnum.Novo, Descricao = "Novo" },
                    new PedidoStatus { Id = (int)PedidoStatusEnum.Preparando, Descricao = "Pedido em preparação" },
                    new PedidoStatus { Id = (int)PedidoStatusEnum.Entregue, Descricao = "Entregue" },
                    new PedidoStatus { Id = (int)PedidoStatusEnum.Cancelado, Descricao = "Cancelado" }
                );
            });

            base.OnModelCreating(builder);
        }
    }
}
