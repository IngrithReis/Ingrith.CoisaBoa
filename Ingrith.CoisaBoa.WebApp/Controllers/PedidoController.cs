using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Data;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class PedidoController : Controller
    {
        [ViewData]
        public int Quantidade { get; set; }

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
        {
            var item = await _context.Item.FirstOrDefaultAsync(i => i.Id == id);
            
            //diminuir de estoque

            item.DiminuirEstoque(1);

            // adicionar ao carrinho
            var pedido = new Pedido
            {
                Usuario = User.Identity.Name,
                Itens = new List<PedidoItem> 
                                 { new PedidoItem
                                               { ItemId =id,
                                                 Quantidade = 1,
                                                ValorTotal = item.Preco,
                                                }
                                  },
               DataVenda = System.DateTime.Now,
               Status = PedidoStatusEnum.Novo,
               TotalPedido = item.Preco,
            };


            // await _context.SaveChangesAsync();

            //incluir pedido

            

            Quantidade = pedido.Itens.Count;
            return RedirectToAction("Index", "Cardapio");
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
