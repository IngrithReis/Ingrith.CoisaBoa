using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Data;
using Ingrith.CoisaBoa.WebApp.Domain;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class CardapioController : Controller
    {
        

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
            var testeDbContext = _context.Item.Include(i => i.Categoria);
            var resultado = await _userManager.FindByNameAsync(User.Identity.Name);
         
            ViewData["Cliente"] = resultado.Nome;
            
           
            
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
            if(resultado == null)
            {
                //Quantidade =0;
            }
            // chamar método de pedido, pra guardar no pedido o que for selecionado
            //criar controller de pedido
            //ViewData["Quantidade"] = +1;
            //resultado.DiminuirEstoque(1);
            //if(resultado.QuantidadeDisponivel == 0)
            //{
            //    ViewData["Quantidade"] = "Esgotado";
            //}

            var resulutadoId = resultado.Id;
            //Quantidade += Quantidade;
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
