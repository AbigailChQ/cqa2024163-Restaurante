using Microsoft.AspNetCore.Mvc;
using Restaurante.Data;
using Restaurante.Models;
using Microsoft.AspNetCore.Authorization;
namespace Restaurante.Controllers 

{ 

    //[Authorize]
    public class RolesController : Controller
    {
        private readonly RestauranteDbContext _context;

        public RolesController(RestauranteDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Rol> lista = _context.Rol.Where(c => !c.eliminado_rol).ToList();
            return View(lista);
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Rol rol)
        {
            if (ModelState.IsValid)
            {
                rol.eliminado_rol = false; // Tipo inicialmente no eliminado
                _context.Rol.Add(rol);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rol = _context.Rol.FirstOrDefault(t => t.id_rol == id && !t.eliminado_rol);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar( Rol rol)
        {
            if (rol != null)
            {
                if (ModelState.IsValid)
                {
                    _context.Rol.Update(rol);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(rol);
        }

        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rol = _context.Rol.FirstOrDefault(t => t.id_rol == id && !t.eliminado_rol);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Rol rol)
        {
            if (rol == null)
            {
                return NotFound();
            }

            rol.eliminado_rol = true;

            _context.Rol.Update(rol);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
