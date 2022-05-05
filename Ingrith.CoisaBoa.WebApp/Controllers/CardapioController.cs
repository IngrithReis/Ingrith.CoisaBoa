using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Data;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;
using System.Linq;
using System.Collections.Generic;
using Ingrith.CoisaBoa.WebApp.Models;
using System.Globalization;
using System.Threading;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class CardapioController : Controller
    {
        [ViewData]
        public int TotalItens { get; set; }
       


        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
       
        
        public CardapioController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }
        // GET: CardapioController
        
        public async Task<ActionResult> Index()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");

            var testeDbContext = _context.Item.Include(i => i.Categoria);
            var resultado = await _userManager.FindByNameAsync(User.Identity.Name);
         
            ViewData["Cliente"] = resultado.Nome;

            var pedidoInput = await _context.Pedido.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Usuario == User.Identity.Name && x.Status == PedidoStatusEnum.Novo);

            if (pedidoInput != null)
                TotalItens = pedidoInput.Itens.Sum(x => x.Quantidade);

            return View(await testeDbContext.ToListAsync());
            
        }

        // GET: CardapioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CardapioController/Create
        public ActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> VerCarrinho()
        {
            
            var pedidoInput = await _context.Pedido
                .Include(x => x.Itens)
                .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(x => x.Usuario == User.Identity.Name && x.Status == PedidoStatusEnum.Novo);
           
            
                return View("Carrinho", pedidoInput);
            
            
        }

        // POST: CardapioController/Create
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

        // GET: CardapioController/Edit/5
        public async Task<IActionResult> AdicionarQt(int id) 
        {   
           // encontrar o item selecionado
            var resultado = await _context.Item.FirstOrDefaultAsync(i => i.Id == id);
           

            var resulutadoId = resultado.Id;
            
            return RedirectToAction("AdicionarCarrinho","Pedido", new {id = resulutadoId});
        }

        // POST: CardapioController/Edit/5
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

        // GET: CardapioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CardapioController/Delete/5
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
