using Restaurante.Data;
using Restaurante.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace Restaurante.Controllers
{
    public class AccesosController : Controller
    {
        //Conexion con el contexto
        private readonly RestauranteDbContext _context;

        public AccesosController(RestauranteDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
       
        [HttpPost]

        public async Task<IActionResult> Index(Usuario usuario)
        {
           
            var usuarioConf = _context.Usuarios.Where(u => u.nombre_u == usuario.nombre_u && u.contraseña == usuario.contraseña).FirstOrDefault();

            if (usuarioConf != null)
            {
               
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,usuarioConf.id_usuario.ToString()),
                        new Claim(ClaimTypes.Name , usuarioConf.nombre_u),


                    };
                var rol = _context.Rol.Where(u => u.id_rol == usuarioConf.id_rol).FirstOrDefault();
                if (rol != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.nombre_rol));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                  
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }

            }
            else
            {
                return RedirectToAction("Privacy", "Home");
            }
            //return RedirectToAction("Privacy", "Home");
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
