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
    public class CheckListController : Controller
    {
        private readonly QuevakWebContext _context;

        public CheckListController(QuevakWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var quevakWebContext = _context.CheckListModel.Include(c => c.Cliente).Include(c => c.Usuario);
            return View(await quevakWebContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CheckListModel == null)
            {
                return NotFound();
            }

            var checkListModel = await _context.CheckListModel
                .Include(c => c.Cliente)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCheckList == id);
            if (checkListModel == null)
            {
                return NotFound();
            }

            return View(checkListModel);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS");
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckListModel checkListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkListModel);
                await _context.SaveChangesAsync();
                TempData["SuccessChecklist"] = "Creación Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS", checkListModel.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario", checkListModel.UsuarioId);
            TempData["ErrorChecklist"] = "La creación no pudo ser completada.";
            return View(checkListModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CheckListModel == null)
            {
                return NotFound();
            }

            var checkListModel = await _context.CheckListModel.FindAsync(id);
            if (checkListModel == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS", checkListModel.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario", checkListModel.UsuarioId);
            return View(checkListModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CheckListModel checkListModel)
        {
            var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");

            if (id != checkListModel.IdCheckList)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    checkListModel.NameLastEdit = nombreCompleto;
                    checkListModel.DateLastEdit = DateTime.Now.ToString();

                    _context.Update(checkListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckListModelExists(checkListModel.IdCheckList))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessChecklist"] = "Edición Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS", checkListModel.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario", checkListModel.UsuarioId);
            TempData["ErrorChecklist"] = "La edición no pudo ser completada.";
            return View(checkListModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CheckListModel == null)
            {
                return NotFound();
            }

            var checkListModel = await _context.CheckListModel
                .Include(c => c.Cliente)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCheckList == id);
            if (checkListModel == null)
            {
                return NotFound();
            }

            return View(checkListModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CheckListModel == null)
            {
                TempData["ErrorChecklist"] = "La eliminación no pudo ser completada.";
                return RedirectToAction(nameof(Index));
            }
            var checkListModel = await _context.CheckListModel.FindAsync(id);
            if (checkListModel != null)
            {
                _context.CheckListModel.Remove(checkListModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckListModelExists(int id)
        {
          return (_context.CheckListModel?.Any(e => e.IdCheckList == id)).GetValueOrDefault();
        }
    }
}
