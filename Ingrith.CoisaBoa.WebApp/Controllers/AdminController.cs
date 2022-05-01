using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ingrith.CoisaBoa.WebApp.Domain;
using Ingrith.CoisaBoa.WebApp.Models;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AdminController(UserManager<Usuario> userManager,
               RoleManager<IdentityRole> roleManager,
               SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        

        [HttpPost]
        
        public async Task<IActionResult> Registrar(AdmRegisterInputModel model)
        {
            var user = new Usuario
            {
                Nome = model.Nome,
                UserName = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,

            };

            if (!(user.Password == user.ConfirmPassword))
            {
                return View("Index");
            }
           //criar usuário
            var resultado = await _userManager.CreateAsync(user, model.Password);

            //Acrescenta a role ao usuário
            await _userManager.AddToRoleAsync(user, "Admin");

            //loga Usuário
            await _signInManager.SignInAsync(user, true);

            return RedirectToAction("Index","Item");

        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var admin = await _userManager.FindByEmailAsync(model.Email);
            
            if (!await _userManager.CheckPasswordAsync(admin, model.Password))
            {
                return View("Index",model);
            }
            //loga Usuário com a Role admin
            await _signInManager.SignInAsync(admin, true);
            return RedirectToAction("Index", "Item");
        }
    }
}
