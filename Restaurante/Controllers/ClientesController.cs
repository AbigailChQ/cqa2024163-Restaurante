using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurante.Data;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    public class ClientesController : Controller
    {
        private readonly RestauranteDbContext _context;

        public ClientesController(RestauranteDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult VerifyNombreUsuarioUnico(string nombre_u, int id_usuario)
        {
            var usuarioExistente = _context.Usuarios.Any(u => u.nombre_u == nombre_u && u.id_usuario != id_usuario);
            return Json(!usuarioExistente);
        }

        public void ConfigurarViewBagIndex()
        {
            var rol = _context.Rol.ToList();

            ViewBag.rol = rol.ToList();
        }
        public void ConfigurarViewBag()
        {
            var rol = _context.Rol.Where(r => r.eliminado_rol == false && r.nombre_rol=="Cliente").ToList();

            // Convert the list of propietarios to a list of SelectListItem
            ViewBag.roles = rol.Select(p => new SelectListItem
            {
                Value = p.id_rol.ToString(),
                Text = $"{p.nombre_rol}"
            }).ToList();
        }

        [HttpGet]
        // GET: Autores/Crear para redirijirnos
        public IActionResult Crear()
        {
            ConfigurarViewBag();
            return View();
        }
        // POST: Categorias/Crear realiza la accion de añadir a la tabla autores
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Usuario usuarios)
        {
            if (usuarios != null)
            {
                if (ModelState.IsValid)
                {
                    usuarios.Rol = _context.Rol.Where(r => r.id_rol == usuarios.id_rol).ToList();
                    _context.Usuarios.Add(usuarios);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(usuarios);
        }
    }
}
