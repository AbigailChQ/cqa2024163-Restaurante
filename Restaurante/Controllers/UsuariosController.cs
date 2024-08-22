using Microsoft.AspNetCore.Mvc;
using Restaurante.Data;
using Restaurante.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurante.Controllers
{
   [Authorize]
    public class UsuariosController : Controller
    {
        private readonly RestauranteDbContext _context;

        public UsuariosController(RestauranteDbContext context)
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
            var rol = _context.Rol.Where(r=>r.eliminado_rol==false).ToList();

            // Convert the list of propietarios to a list of SelectListItem
            ViewBag.roles = rol.Select(p => new SelectListItem
            {
                Value = p.id_rol.ToString(),
                Text = $"{p.nombre_rol}"
            }).ToList();
        }
        // GET: Para obtener la lista de autores
        [HttpGet]
        public IActionResult Index()
        {
            List<Usuario> lista = _context.Usuarios.Where(m => m.eliminado ==false).ToList();
            ConfigurarViewBagIndex();
            return View(lista);
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
                    usuarios.Rol = _context.Rol.Where(r=>r.id_rol==usuarios.id_rol).ToList();
                    _context.Usuarios.Add(usuarios);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usuarios);
        }
        //obtiene mediante el Id los datos del autor
        [HttpGet]
        // GET: Categorias/Edit/5
        public IActionResult Editar(int? id)
        {
           
            if (id == null)
            {
                return NotFound();

            }
            var administradores = _context.Usuarios.FirstOrDefault(c => c.id_usuario == id);
            if (administradores == null)

            {

                return NotFound();

            }
            ConfigurarViewBag();
            return View(administradores);
        }

        // POST: Categorias/Edit/Id dado el id se modificara los datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar( Usuario usuario)
        {
            if (usuario == null)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Usuarios.Update(usuario);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Añadir el mensaje de error al ViewBag para mostrar en la vista
                    ViewBag.ErrorMessage = $"Ocurrió un error al intentar actualizar el usuario: {ex.Message}";

                    // Alternativamente, puedes añadir el mensaje de error al ModelState
                    ModelState.AddModelError(string.Empty, $"Ocurrió un error al intentar actualizar el usuario: {ex.Message}");
                }
            }
            else
            {
                // Capturar y mostrar los errores de validación en la vista
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ViewBag.ValidationErrors += error.ErrorMessage + "\n";
                    }
                }
            }
            ConfigurarViewBag();
            return View(usuario);
        }
        [HttpGet]
        // GET: Categorias/Delete/5 para la eliminacion de datos
        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var usuarios = _context.Usuarios.FirstOrDefault(c => c.id_usuario == id);
            ConfigurarViewBag();
            return View(usuarios);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Usuario usuario)
        {
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.eliminado = true;

            _context.Usuarios.Update(usuario);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
