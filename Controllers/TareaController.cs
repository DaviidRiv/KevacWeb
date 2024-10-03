using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuevakWeb.Data;
using QuevakWeb.Models;

namespace QuevakWeb.Controllers
{
    public class TareaController : Controller
    {
        private readonly QuevakWebContext _context;

        public TareaController(QuevakWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.TareaModel != null ? 
                          View(await _context.TareaModel.ToListAsync()) :
                          Problem("Entity set 'QuevakWebContext.TareaModel'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TareaModel == null)
            {
                return NotFound();
            }

            var tareaModel = await _context.TareaModel
                .FirstOrDefaultAsync(m => m.IdTarea == id);
            if (tareaModel == null)
            {
                return NotFound();
            }

            return View(tareaModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarea,Tarea,Fecha,Aux,NameLastEdit,DateLastEdit")] TareaModel tareaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tareaModel);
                await _context.SaveChangesAsync();
                TempData["SuccessTarea"] = "Creación Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorTarea"] = "La creación no pudo ser completada.";
            return View(tareaModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TareaModel == null)
            {
                return NotFound();
            }

            var tareaModel = await _context.TareaModel.FindAsync(id);
            if (tareaModel == null)
            {
                return NotFound();
            }
            return View(tareaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarea,Tarea,Fecha")] TareaModel tareaModel)
        {
            var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");

            if (id != tareaModel.IdTarea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tareaModel.NameLastEdit = nombreCompleto;
                    tareaModel.DateLastEdit = DateTime.Now.ToString();

                    _context.Update(tareaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaModelExists(tareaModel.IdTarea))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessTarea"] = "Edición Exitosa.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorTarea"] = "La edición no pudo ser completada.";
            return View(tareaModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TareaModel == null)
            {
                return NotFound();
            }

            var tareaModel = await _context.TareaModel
                .FirstOrDefaultAsync(m => m.IdTarea == id);
            if (tareaModel == null)
            {
                return NotFound();
            }

            return View(tareaModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TareaModel == null)
            {
                TempData["ErrorTarea"] = "La eliminación no pudo ser completada.";
                return RedirectToAction(nameof(Index));
            }
            var tareaModel = await _context.TareaModel.FindAsync(id);
            if (tareaModel != null)
            {
                _context.TareaModel.Remove(tareaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaModelExists(int id)
        {
          return (_context.TareaModel?.Any(e => e.IdTarea == id)).GetValueOrDefault();
        }
    }
}
