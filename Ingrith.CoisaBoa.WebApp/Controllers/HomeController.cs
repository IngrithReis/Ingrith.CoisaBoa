using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Models;


namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager, 
            SignInManager<Usuario> signInManager
            
        )
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

        
        public IActionResult Registro()
        {   
            return View();
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
            //var user = new Usuario
            //{
            //    Nome = "Teste2",
            //    UserName = "11333383",
            //    Bairro = "kkk",
            //    Complemento = "fffff"

            //};
            //var resultado = await _userManager.CreateAsync(user,"Senha12345#");

            //if (!resultado.Succeeded)
            //{
            //    return Error();
            //}
            //criando a role sem especificar para qual usuário ela se refere
            //var role = new IdentityRole { Name = "Admin" };
            //resultado = await _roleManager.CreateAsync(role);
            //if (!resultado.Succeeded)
            //{
            //    return Error();
            //}
            // Atribuindo a Role a um usuário específico
            //await _userManager.AddToRoleAsync(user, "Admin");

            //await _signInManager.SignInAsync(user,true);

            return RedirectToAction("Index","Cardapio");
        }

        
        public IActionResult Sobre()
        {
            return View();
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
