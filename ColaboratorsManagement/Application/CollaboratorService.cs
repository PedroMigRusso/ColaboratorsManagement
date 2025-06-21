using ColaboratorsManagement.Application.Interface;
using ColaboratorsManagement.Database;
using ColaboratorsManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ColaboratorsManagement.Application
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly Database.AppDbContext _context;
        public CollaboratorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CollaboratorModel>> GetAllAsync(string search = null, string technology = null)
        {
            var query = _context.Collaborators.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(technology))
            {
                query = query.Where(c => c.BetterTechnology.Contains(technology));
            }

            return await query.ToListAsync();
        }

        public async Task<CollaboratorModel> GetByIdAsync(int id)
        {
            var colaborador = await _context.Collaborators.FindAsync(id);
            if (colaborador != null)
            {
                return colaborador;
            }
            return colaborador;
        }

        public async Task AddAsync(CollaboratorModel colaborador)
        {
            _context.Collaborators.Add(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CollaboratorModel colaborador)
        {
            _context.Collaborators.Update(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var colaborador = await _context.Collaborators.FindAsync(id);
            if (colaborador != null)
            {
                _context.Collaborators.Remove(colaborador);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetAllTechnologiesAsync()
        {
            return await _context.Collaborators
                .Select(c => c.BetterTechnology)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();
        }
    }
}
