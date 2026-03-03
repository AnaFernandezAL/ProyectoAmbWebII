using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPujas
    {
        Task<ICollection<Pujas>> ListAsync();
        Task<Pujas?> FindByIdAsync(int id);
        Task<int> AddAsync(Pujas entity);
        Task DeleteAsync(int id);

        Task<ICollection<Pujas>> FindByIDSubastaAsync(int subastaId);
    }
}
