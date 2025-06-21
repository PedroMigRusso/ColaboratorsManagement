using ColaboratorsManagement.Models;

namespace ColaboratorsManagement.Application.Interface
{
    public interface ICollaboratorService
    {
        Task<List<CollaboratorModel>> GetAllAsync(string search = null, string technology = null);
        Task<CollaboratorModel> GetByIdAsync(int id);
        Task AddAsync(CollaboratorModel colaborador);
        Task UpdateAsync(CollaboratorModel colaborador);
        Task DeleteAsync(int id);
        Task<List<string>> GetAllTechnologiesAsync();

    }
}
