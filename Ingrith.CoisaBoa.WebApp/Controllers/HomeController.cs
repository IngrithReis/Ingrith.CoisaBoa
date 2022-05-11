using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Models;
using Ingrith.CoisaBoa.WebApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager, 
            SignInManager<Usuario> signInManager,
            AppDbContext context

            
        )
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }
        
        public string ReturnUrl { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioLoginInputModel model)
        {
            var resultado = await _userManager.FindByNameAsync(model.Telefone);
            if(resultado == null)
            {
                var usuario = new Usuario
                {
                    Nome = "Novo amigo(a)",
                    UserName = model.Telefone,
                    
                };
                ViewData["Cliente"] = usuario.Nome;
                var usuarioLogin = await _userManager.CreateAsync(usuario, "#Senha12345");
                if (!usuarioLogin.Succeeded)
                {
                   
                    return View("ErroLogin");
                }
                await _signInManager.SignInAsync(usuario, false);
                return RedirectToAction("Index", "Cardapio");
            }

            ViewData["Cliente"] = resultado.Nome;
            await _signInManager.SignInAsync(resultado,false);
            return RedirectToAction("Index", "Cardapio");
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrarendereco()
        {   
            //encontrar ususário
            var usuario = await _userManager.FindByNameAsync(User.Identity.Name);

            // verificar se há cadastro de enderço
            if(usuario.Endereco == null)
            {
                var listaBairros = await _context.BairrosAtendidos.ToListAsync();
                ViewBag.BairrosAtendidos = listaBairros.Select(x => new SelectListItem { Text = x.Nome });
                return View("Registro");
            }
            return View("Registro");
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioInputRegisterModel model)
        {
            //encontrar ususário
            var usuario = await _userManager.FindByNameAsync(User.Identity.Name);

            // Alterar informações de endereço 
            usuario.Nome = model.Nome;
            usuario.Endereco = model.Endereco;
            usuario.Bairro = model.Bairro;
            usuario.Complemento = model.Complemento;

            ViewData["Nome"] = usuario.Nome;
            ViewData["Bairro"] = usuario.Bairro;
            ViewData["Endereco"] = usuario.Endereco;

            // encontrar pedido do ususario

            var pedido = await _context.Pedido.FirstOrDefaultAsync(p => p.Usuario == usuario.UserName);
           
            ViewData["TotalPedido"] = pedido.TotalPedido;
            ViewData["Pagamento"] = pedido.Pagamento;
            ViewData["Obs"] = pedido.Observacao;
            ViewData["Data"] = pedido.DataVenda;
            



            return View("Sucesso", pedido);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(UsuarioInputRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return Error();
            }

            var user = new Usuario
            {
                Nome = model.Nome,
                UserName = model.Telefone,
                Bairro = model.Bairro,
                Endereco = model.Endereco,
                Complemento = model.Complemento
            };

            var resultado = await _userManager.CreateAsync(user, "#Senha12345");
            if (!resultado.Succeeded)
            {
                ViewData["Cliente"] = user.Nome;
                return Error();
            }
            await _signInManager.SignInAsync(user, false);
            

            return RedirectToAction("Index","Cardapio");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void Sair()
        {
            return;
        }

    }
}
