using System.Diagnostics;
using ColaboratorsManagement.Application.Interface;
using ColaboratorsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ColaboratorsManagement.Controllers
{
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorService _service;

        public CollaboratorController(ICollaboratorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string search, string technology, int page = 1, int pageSize = 5)
        {
            try
            {
                var colaboradores = await _service.GetAllAsync(search, technology);
                var totalItems = colaboradores.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                var paged = colaboradores
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                ViewBag.CurrentSearch = search;
                ViewBag.CurrentTech = technology;

                ViewBag.Technology = new SelectList(
                    colaboradores.Select(c => c.BetterTechnology).Distinct().OrderBy(t => t).ToList()
                );
                return View(paged);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Index action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var colaborador = await _service.GetByIdAsync(id);
                if (colaborador == null) return NotFound();
                return View(colaborador);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Edit (GET) action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CollaboratorModel colaborador)
        {
            try
            {
                if (!ModelState.IsValid) return View(colaborador);
                await _service.UpdateAsync(colaborador);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Edit (POST) action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Create (GET) action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollaboratorModel colaborador)
        {
            try
            {
                if (!ModelState.IsValid) return View(colaborador);
                await _service.AddAsync(colaborador);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Create (POST) action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var colaborador = await _service.GetByIdAsync(id);
                if (colaborador == null) return NotFound();
                return View(colaborador);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Delete (GET) action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteConfirmed (POST) action: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
