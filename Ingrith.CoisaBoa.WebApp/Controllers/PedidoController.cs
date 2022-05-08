using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Data;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;
using System.Linq;
using System.Globalization;
using System;
using Ingrith.CoisaBoa.WebApp.Models;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class PedidoController : Controller
    {
        

        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PedidoController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: PedidoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PedidoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PedidoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AdicionarCarrinho(int id)
        {   //variável para alterar o formato DateTime do pedido
            var dataCompra = DateTime.Now;

            var item = await _context.Item.FirstOrDefaultAsync(i => i.Id == id);
            //verificar Estoque
            var quantidade = item.QuantidadeDisponivel;
            if(quantidade == 0)
            {
                return View("Esgotado");
            }
            //diminuir de estoque

            item.DiminuirEstoque(1);
            //verificar se há pedido e status

            var pedidoInput = await _context.Pedido.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Usuario == User.Identity.Name && x.Status == PedidoStatusEnum.Novo);
            //verificar se usuario já tem pedido novo aberto, se não, adicionar ao carrinho
            if (pedidoInput == null)
            {
                // criar o pedido
                pedidoInput = new Pedido
                {
                    Usuario = User.Identity.Name,
                    DataVenda = dataCompra,
                    Status = PedidoStatusEnum.Novo,
                    TotalPedido = item.Preco,
                    Itens = new List<PedidoItem>(),
                    TaxaEntrega = 7.0m,
                    

                };
                // guardar no banco o pedido criado
                _context.Pedido.Add(pedidoInput);
            }
            // tem pedido novo aberto, então, adicionar itens
            var pedidoItem = pedidoInput.Itens.FirstOrDefault(x => x.Id == id);

            if(pedidoItem == null)
            {
                pedidoInput.Itens.Add(new PedidoItem
                {
                    ItemId = id,
                    Quantidade = 1,
                    ValorTotal = item.Preco,
                });
            }
            else
            {
                pedidoItem.Quantidade++;
                pedidoItem.ValorTotal = item.Preco * pedidoItem.Quantidade;
            }

            
            pedidoInput.TotalPedido = pedidoInput.Itens.Sum(x => x.ValorTotal) + pedidoInput.TaxaEntrega;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Cardapio");
        }
        [HttpPost]
        public async Task<ActionResult> FecharPedido(PedidoInputModel model)
        {   //Procurar pedido 
            
            var pedido = await _context.Pedido.FirstOrDefaultAsync(p => p.Id == model.Id);
            //Adicionar observacao e forma de pagamento e taxa de entrega

            

            //status de pedido deve ser alterdao

            pedido.Status = PedidoStatusEnum.Preparando;
            pedido.Observacao = model.Observacao;
            pedido.Pagamento = model.Pagamento;

            await _context.SaveChangesAsync();

            // verificar se ususário já tem cadastro de endereço

            var usuario = _context.Users.FirstOrDefault(u => u.UserName == pedido.Usuario);
            if(usuario.Endereco == null)
            {   
                //redirect Action Regristar
                return RedirectToAction("CadastrarEndereco","Home");
            }

            return View("Sucesso");

        }
        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
