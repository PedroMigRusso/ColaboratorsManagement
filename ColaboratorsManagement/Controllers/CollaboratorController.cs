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

        /*Index
         * This action retrieves a list of collaborators, optionally filtered by search term and technology.
         * It supports pagination and returns a view with the paginated list.
         * 
         * Parameters:
         * - search: Optional search term to filter collaborators by name.
         * - technology: Optional technology to filter collaborators by their better technology.
         * - page: The current page number for pagination (default is 1).
         * - pageSize: The number of items per page (default is 5).
         * 
         * Returns:
         * - A view with the paginated list of collaborators.
        */
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
        /* Edit
         * This action retrieves a collaborator by ID for editing.
         * It returns a view with the collaborator's details for modification.
         * 
         * Parameters:
         * - id: The ID of the collaborator to edit.
         * 
         * Returns:
         * - A view with the collaborator's details for editing.
        */
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
        /* Edit
         * This action processes the form submission for editing a collaborator.
         * It validates the model and updates the collaborator's details in the database.
         * 
         * Parameters:
         * - colaborador: The collaborator model containing updated details.
         * 
         * Returns:
         * - A redirect to the Index action if successful, or the Edit view with validation errors if not.
        */
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
        /* Create
         * This action returns a view for creating a new collaborator.
         * It does not require any parameters and simply returns the view.
         * 
         * Returns:
         * - A view for creating a new collaborator.
        */
        public IActionResult Create() => View();
        /*Create
         * This action processes the form submission for creating a new collaborator.
         * It validates the model and adds the new collaborator to the database.
         * 
         * Parameters:
         * - colaborador: The collaborator model containing details for the new collaborator.
         * 
         * Returns:
         * - A redirect to the Index action if successful, or the Create view with validation errors if not.
        */
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
        /*Delete
         * This action retrieves a collaborator by ID for deletion confirmation.
         * It returns a view with the collaborator's details to confirm deletion.
         * 
         * Parameters:
         * - id: The ID of the collaborator to delete.
         * 
         * Returns:
         * - A view with the collaborator's details for deletion confirmation.
        */
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
        /*DeleteConfirmed
         * This action processes the form submission for deleting a collaborator.
         * It deletes the collaborator from the database and redirects to the Index action.
         * 
         * Parameters:
         * - id: The ID of the collaborator to delete.
         * 
         * Returns:
         * - A redirect to the Index action if successful, or an error view if an exception occurs.
        */
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
