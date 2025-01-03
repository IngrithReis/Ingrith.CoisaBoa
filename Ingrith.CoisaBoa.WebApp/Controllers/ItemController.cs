﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ingrith.CoisaBoa.WebApp.Data;
using Ingrith.CoisaBoa.WebApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Ingrith.CoisaBoa.WebApp.Domain.Enums;

namespace Ingrith.CoisaBoa.WebApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ItemController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        public ItemController(AppDbContext context, UserManager<Usuario> userMAnager)
        {
            _context = context;
            _userManager = userMAnager;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var testeDbContext = _context.Item.Include(i => i.Categoria);
            var resultado = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["Administrador"] = resultado.Nome;
            return View(await testeDbContext.ToListAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewData["CategoriaItemId"] = new SelectList(_context.Set<CategoriaItem>(), "Id", "Nome");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,CategoriaItemId,QuantidadeDisponivel,ControlEstoque,CaminhoImagem")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaItemId"] = new SelectList(_context.Set<CategoriaItem>(), "Id", "Id", item.CategoriaItemId);
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoriaItemId"] = new SelectList(_context.Set<CategoriaItem>(), "Id", "Id", item.CategoriaItemId);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,CategoriaItemId,QuantidadeDisponivel,ControlEstoque,CaminhoImagem")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaItemId"] = new SelectList(_context.Set<CategoriaItem>(), "Id", "Id", item.CategoriaItemId);
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
        public async Task<IActionResult> GraficoItens()
        {
            
            var itens = await _context.PedidoItem
                .Where(x => x.Pedido.Status == PedidoStatusEnum.Entregue)
                .Include(x => x.Item)
                .Include(x => x.Pedido)
                .GroupBy(x => x.Item.Nome)
                .Select(x => new
                {
                    Nome = x.Key,
                    Quantidade = x.Sum(y => y.Quantidade)
                }).ToListAsync();

            ViewBag.Nomes = itens.Select(x => x.Nome).ToArray();
            ViewBag.Valores = itens.Select(x => x.Quantidade).ToArray();

            return View();
        }
    }
}
