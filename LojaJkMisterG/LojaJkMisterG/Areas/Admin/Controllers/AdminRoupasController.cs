using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaJkMisterG.Context;
using LojaJkMisterG.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ReflectionIT.Mvc.Paging;

namespace LojaJkMisterG.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.Vendedor)]
    public class AdminRoupasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminRoupasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminRoupas
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Roupas.Include(r => r.Categoria);
        //    return View(await appDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Roupas.Include(r => r.Categoria).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);

        }

        // GET: Admin/AdminRoupas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roupas == null)
            {
                return NotFound();
            }

            var roupa = await _context.Roupas
                .Include(r => r.Categoria)
                .FirstOrDefaultAsync(m => m.RoupaId == id);
            if (roupa == null)
            {
                return NotFound();
            }

            return View(roupa);
        }

        // GET: Admin/AdminRoupas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Admin/AdminRoupas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoupaId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsRoupaPreferida,EmEstoque,CategoriaId")] Roupa roupa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roupa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", roupa.CategoriaId);
            return View(roupa);
        }

        // GET: Admin/AdminRoupas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roupas == null)
            {
                return NotFound();
            }

            var roupa = await _context.Roupas.FindAsync(id);
            if (roupa == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", roupa.CategoriaId);
            return View(roupa);
        }

        // POST: Admin/AdminRoupas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoupaId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsRoupaPreferida,EmEstoque,CategoriaId")] Roupa roupa)
        {
            if (id != roupa.RoupaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roupa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoupaExists(roupa.RoupaId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", roupa.CategoriaId);
            return View(roupa);
        }

        // GET: Admin/AdminRoupas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roupas == null)
            {
                return NotFound();
            }

            var roupa = await _context.Roupas
                .Include(r => r.Categoria)
                .FirstOrDefaultAsync(m => m.RoupaId == id);
            if (roupa == null)
            {
                return NotFound();
            }

            return View(roupa);
        }

        // POST: Admin/AdminRoupas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roupas == null)
            {
                return Problem("Entity set 'AppDbContext.Roupas'  is null.");
            }
            var roupa = await _context.Roupas.FindAsync(id);
            if (roupa != null)
            {
                _context.Roupas.Remove(roupa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoupaExists(int id)
        {
          return _context.Roupas.Any(e => e.RoupaId == id);
        }

    }
}
