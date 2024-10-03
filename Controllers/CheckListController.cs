using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuevakWeb.Data;
using QuevakWeb.Models;
using QuevakWeb.Models.AuxModels;

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
            var quevakWebContext = _context.CheckListModel
                .Include(c => c.Cliente)
                .Include(c => c.Usuario)
                .Include(c => c.CheckListTareas)
                    .ThenInclude(t => t.Tarea)
                .Include(c => c.CheckListAreas)
                    .ThenInclude(t => t.Area);

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
            ViewBag.TareasDisponibles = _context.TareaModel.Select(t => new { IdT = t.IdTarea, NombreT = t.Tarea }).ToList();
            ViewBag.AreasDisponibles = _context.AreaModel.Select(t => new { IdA = t.IdArea, NombreA = t.Area }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckListModel checkListModel, int[]? TareasSeleccionadas, int[]? AreasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                // Guardar el CheckListModel en la base de datos
                _context.Add(checkListModel);
                await _context.SaveChangesAsync();

                // Guardar las tareas seleccionadas en la tabla intermedia CheckListTareaModel
                if (TareasSeleccionadas != null)
                {
                    foreach (var tareaId in TareasSeleccionadas)
                    {
                        var checkListTarea = new CheckListTareaModel
                        {
                            CheckListId = checkListModel.IdCheckList,
                            TareaId = tareaId,
                            Completada = false // Inicialmente todas las tareas están no completadas
                        };
                        _context.CheckListTareaModel.Add(checkListTarea);
                    }
                }

                if (AreasSeleccionadas != null)
                {
                    foreach (var areaId in AreasSeleccionadas)
                    {
                        var checkListArea = new CheckListAreaModel
                        {
                            CheckListId = checkListModel.IdCheckList,
                            AreaId = areaId,
                            Completada = false // Inicialmente todas las tareas están no completadas
                        };
                        _context.CheckListAreaModel.Add(checkListArea);
                    }
                }

                await _context.SaveChangesAsync();
                TempData["SuccessChecklist"] = "Creación Exitosa.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS", checkListModel.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario", checkListModel.UsuarioId);
            ViewBag.TareasDisponibles = _context.TareaModel.Select(t => new { IdT = t.IdTarea, NombreT = t.Tarea }).ToList();
            ViewBag.AreasDisponibles = _context.AreaModel.Select(t => new { IdA = t.IdArea, NombreA = t.Area }).ToList();
            TempData["ErrorChecklist"] = "La creación no pudo ser completada.";
            return View(checkListModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CheckListModel == null)
            {
                return NotFound();
            }

            // Incluir Cliente, Usuario y las tareas del checklist
            var checkListModel = await _context.CheckListModel
                .Include(c => c.CheckListTareas)
                    .ThenInclude(clt => clt.Tarea)
                .Include(c => c.CheckListAreas)
                    .ThenInclude(clt => clt.Area)
                .FirstOrDefaultAsync(c => c.IdCheckList == id);

            if (checkListModel == null)
            {
                return NotFound();
            }

            // Obtener todas las tareas y areas disponibles
            var allTareas = await _context.TareaModel.ToListAsync();
            var allAreas = await _context.AreaModel.ToListAsync();

            // Crear un objeto para saber qué tareas y areas están seleccionadas
            ViewBag.TareasDisponibles = allTareas.Select(t => new
            {
                t.IdTarea,
                t.Tarea,
                Seleccionada = checkListModel.CheckListTareas.Any(ct => ct.TareaId == t.IdTarea)
            }).ToList();

            ViewBag.AreasDisponibles = allAreas.Select(t => new
            {
                t.IdArea,
                t.Area,
                Seleccionada = checkListModel.CheckListAreas.Any(ct => ct.AreaId == t.IdArea)
            }).ToList();

            // Dropdowns de Cliente y Usuario
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS", checkListModel.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario", checkListModel.UsuarioId);

            return View(checkListModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CheckListModel checkListModel, int[]? TareasSeleccionadas, int[]? AreasSeleccionadas)
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
                    // Obtener el CheckList actual desde la base de datos
                    var existingCheckList = await _context.CheckListModel
                        .Include(c => c.CheckListTareas) // Incluir las tareas actuales
                        .Include(c => c.CheckListAreas) // Incluir las areas actuales
                        .FirstOrDefaultAsync(c => c.IdCheckList == id);

                    if (existingCheckList == null)
                    {
                        return NotFound();
                    }

                    // Actualizar las tareas seleccionadas
                    var tareasSeleccionadas = new HashSet<int>(TareasSeleccionadas);
                    var areasSeleccionadas = new HashSet<int>(AreasSeleccionadas);

                    // Eliminar tareas que ya no están seleccionadas
                    var tareasAEliminar = existingCheckList.CheckListTareas
                        .Where(ct => !tareasSeleccionadas.Contains((int)ct.TareaId))
                        .ToList(); // Crear una lista temporal para las tareas a eliminar
                    
                    // Eliminar areas que ya no están seleccionadas
                    var areasAEliminar = existingCheckList.CheckListAreas
                        .Where(ct => !areasSeleccionadas.Contains((int)ct.AreaId))
                        .ToList(); // Crear una lista temporal para las areas a eliminar

                    foreach (var tarea in tareasAEliminar)
                    {
                        existingCheckList.CheckListTareas.Remove(tarea);
                    }

                    foreach (var area in areasAEliminar)
                    {
                        existingCheckList.CheckListAreas.Remove(area);
                    }

                    // Añadir nuevas tareas seleccionadas
                    foreach (var tareaId in TareasSeleccionadas)
                    {
                        if (!existingCheckList.CheckListTareas.Any(ct => ct.TareaId == tareaId))
                        {
                            existingCheckList.CheckListTareas.Add(new CheckListTareaModel
                            {
                                CheckListId = id,
                                TareaId = tareaId,
                                Completada = false // Estado inicial de la tarea
                            });
                        }
                    }

                    // Añadir nuevas areas seleccionadas
                    foreach (var areaId in AreasSeleccionadas)
                    {
                        if (!existingCheckList.CheckListAreas.Any(ct => ct.AreaId == areaId))
                        {
                            existingCheckList.CheckListAreas.Add(new CheckListAreaModel
                            {
                                CheckListId = id,
                                AreaId = areaId,
                                Completada = false // Estado inicial de la tarea
                            });
                        }
                    }

                    // Actualizar otros campos del CheckList
                    existingCheckList.Fecha = checkListModel.Fecha;
                    existingCheckList.ClienteId = checkListModel.ClienteId;
                    existingCheckList.UsuarioId = checkListModel.UsuarioId;
                    existingCheckList.Observacion = checkListModel.Observacion;
                    existingCheckList.Firma = checkListModel.Firma;
                    // Actualizar el nombre y fecha de la última edición
                    existingCheckList.NameLastEdit = nombreCompleto;
                    existingCheckList.DateLastEdit = DateTime.Now.ToString();

                    // Guardar cambios
                    _context.Update(existingCheckList);
                    await _context.SaveChangesAsync();

                    TempData["SuccessChecklist"] = "Edición Exitosa.";
                    return RedirectToAction(nameof(Index));
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
            }

            // En caso de error, recargar la vista con los datos originales
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "IdCliente", "NombreRS", checkListModel.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.UsuarioModel, "IdUsuario", "InfoUsuario", checkListModel.UsuarioId);

            // Cargar las tareas disponibles y marcar las seleccionadas
            var allTareas = await _context.TareaModel.ToListAsync();
            ViewBag.TareasDisponibles = allTareas.Select(t => new
            {
                t.IdTarea,
                t.Tarea,
                Seleccionada = checkListModel.CheckListTareas.Any(ct => ct.TareaId == t.IdTarea)
            }).ToList();
            
            // Cargar las areas disponibles y marcar las seleccionadas
            var allAreas = await _context.AreaModel.ToListAsync();
            ViewBag.AreasDisponibles = allAreas.Select(t => new
            {
                t.IdArea,
                t.Area,
                Seleccionada = checkListModel.CheckListAreas.Any(ct => ct.AreaId == t.IdArea)
            }).ToList();

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
