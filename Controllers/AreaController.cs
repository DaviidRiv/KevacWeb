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
    public class AreaController : Controller
    {
        private readonly QuevakWebContext _context;

        public AreaController(QuevakWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.AreaModel != null ? 
                          View(await _context.AreaModel.ToListAsync()) :
                          Problem("Entity set 'QuevakWebContext.AreaModel'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AreaModel == null)
            {
                return NotFound();
            }

            var areaModel = await _context.AreaModel
                .FirstOrDefaultAsync(m => m.IdArea == id);
            if (areaModel == null)
            {
                return NotFound();
            }

            return View(areaModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArea,Area,HorarioI,HorarioF,Aux,Fecha,NameLastEdit,DateLastEdit")] AreaModel areaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaModel);
                await _context.SaveChangesAsync();
                TempData["SuccessArea"] = "Creación Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorArea"] = "La creación no pudo ser completada.";
            return View(areaModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AreaModel == null)
            {
                return NotFound();
            }

            var areaModel = await _context.AreaModel.FindAsync(id);
            if (areaModel == null)
            {
                return NotFound();
            }
            return View(areaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArea,Area,HorarioI,HorarioF,Aux,Fecha,NameLastEdit,DateLastEdit")] AreaModel areaModel)
        {
            var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");

            if (id != areaModel.IdArea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    areaModel.NameLastEdit = nombreCompleto;
                    areaModel.DateLastEdit = DateTime.Now.ToString();

                    _context.Update(areaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaModelExists(areaModel.IdArea))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessArea"] = "Edición Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorArea"] = "La edición no pudo ser completada.";
            return View(areaModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AreaModel == null)
            {
                return NotFound();
            }

            var areaModel = await _context.AreaModel
                .FirstOrDefaultAsync(m => m.IdArea == id);
            if (areaModel == null)
            {
                return NotFound();
            }

            return View(areaModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AreaModel == null)
            {
                TempData["ErrorArea"] = "La eliminación no pudo ser completada.";
                return RedirectToAction(nameof(Index));
            }
            var areaModel = await _context.AreaModel.FindAsync(id);
            if (areaModel != null)
            {
                _context.AreaModel.Remove(areaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaModelExists(int id)
        {
          return (_context.AreaModel?.Any(e => e.IdArea == id)).GetValueOrDefault();
        }
    }
}
