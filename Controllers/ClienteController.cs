using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuevakWeb.Data;
using QuevakWeb.Models;

namespace QuevakWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly QuevakWebContext _context;

        public ClienteController(QuevakWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.ClienteModel != null ?
                        View(await _context.ClienteModel.ToListAsync()) :
                        Problem("Entity set 'QuevakWebContext.ClienteModel'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,RFC,NombreRS,Domicilio,Municipio,Estado,Telefono,NombreContacto,Correo")] ClienteModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteModel);
                await _context.SaveChangesAsync();
                TempData["SuccessCliente"] = "Creación Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorCliente"] = "La creación no pudo ser completada.";
            return View(clienteModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel.FindAsync(id);
            if (clienteModel == null)
            {
                return NotFound();
            }
            return View(clienteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,RFC,NombreRS,Domicilio,Municipio,Estado,Telefono,NombreContacto,Correo,NameLastEdit,DateLastEdit")] ClienteModel clienteModel)
        {
            var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");

            if (id != clienteModel.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clienteModel.NameLastEdit = nombreCompleto;
                    clienteModel.DateLastEdit = DateTime.Now.ToString();

                    _context.Update(clienteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteModelExists(clienteModel.IdCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessCliente"] = "Edición Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorCliente"] = "La edición no pudo ser completada.";
            return View(clienteModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteModel == null)
            {
                TempData["ErrorCliente"] = "La eliminación no pudo ser completada.";
                return RedirectToAction(nameof(Index));
            }
            var clienteModel = await _context.ClienteModel.FindAsync(id);
            if (clienteModel != null)
            {
                _context.ClienteModel.Remove(clienteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteModelExists(int id)
        {
            return (_context.ClienteModel?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
