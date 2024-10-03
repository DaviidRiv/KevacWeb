using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuevakWeb.Data;
using QuevakWeb.Models;

namespace QuevakWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly QuevakWebContext _context;

        public UsuarioController(QuevakWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.UsuarioModel != null ? 
                          View(await _context.UsuarioModel.ToListAsync()) :
                          Problem("Entity set 'QuevakWebContext.UsuarioModel'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioModel == null)
            {
                return NotFound();
            }

            var usuarioModel = await _context.UsuarioModel
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            return View(usuarioModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,ApellidoP,ApellidoM,Password,RolUser")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<UsuarioModel>();
                usuarioModel.Password = hasher.HashPassword(usuarioModel, usuarioModel.Password); // Encriptar la contraseña


                _context.Add(usuarioModel);
                await _context.SaveChangesAsync();
                TempData["SuccessUsuario"] = "Creación Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorUsuario"] = "La creación no pudo ser completada.";
            return View(usuarioModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioModel == null)
            {
                return NotFound();
            }

            var usuarioModel = await _context.UsuarioModel.FindAsync(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }
            return View(usuarioModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,ApellidoP,ApellidoM,Password,RolUser")] UsuarioModel usuarioModel)
        {
            var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");

            if (id != usuarioModel.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _context.UsuarioModel.FindAsync(id);
                    if (usuarioExistente == null)
                    {
                        return NotFound();
                    }

                    // Actualizar los datos del usuario excepto la contraseña
                    usuarioExistente.Nombre = usuarioModel.Nombre;
                    usuarioExistente.ApellidoP = usuarioModel.ApellidoP;
                    usuarioExistente.ApellidoM = usuarioModel.ApellidoM;

                    // Si se ha introducido una nueva contraseña, encriptarla
                    if (!string.IsNullOrEmpty(usuarioModel.Password))
                    {
                        var hasher = new PasswordHasher<UsuarioModel>();
                        usuarioExistente.Password = hasher.HashPassword(usuarioExistente, usuarioModel.Password);
                    }

                    usuarioExistente.NameLastEdit = nombreCompleto;
                    usuarioExistente.DateLastEdit = DateTime.Now.ToString();

                    _context.Update(usuarioExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioModelExists(usuarioModel.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessUsuario"] = "Edición Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorUsuario"] = "La edición no pudo ser completada.";
            return View(usuarioModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioModel == null)
            {
                return NotFound();
            }

            var usuarioModel = await _context.UsuarioModel
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            return View(usuarioModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioModel == null)
            {
                TempData["ErrorUsuario"] = "La eliminación no pudo ser completada.";
                return RedirectToAction(nameof(Index));
            }

            var usuarioModel = await _context.UsuarioModel.FindAsync(id);
            if (usuarioModel != null)
            {
                _context.UsuarioModel.Remove(usuarioModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioModelExists(int id)
        {
          return (_context.UsuarioModel?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
