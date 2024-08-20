using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Models;
using System.Linq;

namespace Restaurante.Controllers
{
    public class ComidasController : Controller
    {
        private readonly RestauranteDbContext _context;

        public ComidasController(RestauranteDbContext context)
        {
            _context = context;
        }

        // GET: Comidas
        public IActionResult Index()
        {
            var comidas = _context.Comidas.ToList();
            return View(comidas);
        }

        // GET: Comidas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comida = _context.Comidas
                .FirstOrDefault(m => m.Id == id);
            if (comida == null)
            {
                return NotFound();
            }

            return View(comida);
        }

        // GET: Comidas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comidas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Comida comida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comida);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(comida);
        }

        // GET: Comidas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comida = _context.Comidas.Find(id);
            if (comida == null)
            {
                return NotFound();
            }
            return View(comida);
        }

        // POST: Comidas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Descripcion,Precio")] Comida comida)
        {
            if (id != comida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comida);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComidaExists(comida.Id))
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
            return View(comida);
        }

        // GET: Comidas/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comida = _context.Comidas
                .FirstOrDefault(m => m.Id == id);
            if (comida == null)
            {
                return NotFound();
            }

            return View(comida);
        }

        // POST: Comidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comida = _context.Comidas.Find(id);
            _context.Comidas.Remove(comida);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ComidaExists(int id)
        {
            return _context.Comidas.Any(e => e.Id == id);
        }
    }
}
