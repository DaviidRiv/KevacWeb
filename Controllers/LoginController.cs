using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using QuevakWeb.Data;
using QuevakWeb.Models;

namespace QuevakWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuevakWebContext _context;
        private readonly IDistributedCache _cache;

        public LoginController(QuevakWebContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IActionResult Login()
        {
            if (TempData.ContainsKey("ErrorSession"))
            {
                ViewBag.ErrorSession = TempData["ErrorSession"]?.ToString();
            }
            return View();
        }


        [HttpPost]
        public  IActionResult IniciarSesion(string nombreCompleto, string password)
        {
            if (string.IsNullOrEmpty(nombreCompleto) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorSession"] = "Nombre completo o contraseña no pueden estar vacíos.";
                return RedirectToAction(nameof(Login));
            }

            var nombres = nombreCompleto.Split(' ');

            if (nombres.Length < 3)
            {
                TempData["ErrorSession"] = "Por favor, ingrese el nombre completo incluyendo nombre, apellido paterno y apellido materno.";
                return RedirectToAction(nameof(Login));
            }

            var nombre = nombres[0];
            var apellidoP = nombres[1];
            var apellidoM = nombres[2];

            // Buscar al usuario en la base de datos
            var usuario = _context.UsuarioModel.FirstOrDefault(u =>
                                                                u.Nombre == nombre &&
                                                                u.ApellidoP == apellidoP &&
                                                                u.ApellidoM == apellidoM);

            if (usuario != null)
            {
                var hasher = new PasswordHasher<UsuarioModel>();
                var result = hasher.VerifyHashedPassword(null, usuario.Password, password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("IdUser", usuario.IdUsuario.ToString());
                    HttpContext.Session.SetString("Nombre", usuario.Nombre);
                    HttpContext.Session.SetString("ApellidoP", usuario.ApellidoP);
                    HttpContext.Session.SetString("ApellidoM", usuario.ApellidoM);
                    HttpContext.Session.SetString("RolUser", usuario.RolUser);
                    HttpContext.Session.SetString("NombreCompleto", usuario.Nombre + " " + usuario.ApellidoP + " " + usuario.ApellidoM);

                    return RedirectToAction("Index", "Home");
                }
            }

            // Autenticación fallida
            TempData["ErrorSession"] = "Nombre completo o contraseña incorrectos.";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult LogOut()
        {
            // Elimina los datos de la sesión al cerrar sesión
            HttpContext.Session.Remove("IdUsuario");
            HttpContext.Session.Remove("Nombre");
            HttpContext.Session.Remove("ApellidoP");
            HttpContext.Session.Remove("ApellidoM");
            HttpContext.Session.Remove("RolUser");
            HttpContext.Session.Remove("NombreC");

            // Puedes también invalidar completamente la sesión si lo deseas
            HttpContext.Session.Clear();

            ViewBag.CerrarSesion = "Sesión Cerrada Correctamente.";
            return View("Login");
        }
    }
}
