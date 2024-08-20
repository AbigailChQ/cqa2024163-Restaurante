using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Models;
using System.Security.Claims;

namespace Restaurante.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {
          // Asegura que solo los usuarios autenticados puedan acceder
       
            private readonly RestauranteDbContext _context;

            public OrdenesController(RestauranteDbContext context)
            {
                _context = context;
            }

            // GET: Muestra las comidas disponibles para ordenar
            [HttpGet]
            public IActionResult Ordenar()
            {
                var comidas = _context.Comidas.ToList();
                return View(comidas);
            }

            // POST: Procesa la orden
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Ordenar(int comidaId, int cantidad)
            {
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Obtiene el ID del usuario logueado

                if (usuarioId == null || comidaId == 0 || cantidad <= 0)
                {
                    return BadRequest("Datos de la orden inválidos.");
                }

                var orden = new Orden
                {
                    UsuarioId = int.Parse(usuarioId),
                    ComidaId = comidaId,
                    Cantidad = cantidad,
                    FechaOrden = DateTime.Now
                };

                _context.Ordenes.Add(orden);
                _context.SaveChanges();

                return RedirectToAction("Recibo", new { id = orden.Id });
            }

            // GET: Muestra el recibo después de que se realiza la orden
            [HttpGet]
            public IActionResult Recibo(int id)
            {
                var orden = _context.Ordenes
                               .Include(o => o.Comida)
                                    .Include(o => o.Usuario)
                                    .FirstOrDefault(o => o.Id == id);

                if (orden == null)
                {
                    return NotFound("Orden no encontrada.");
                }

                return View(orden);
            }
        }
    
}
