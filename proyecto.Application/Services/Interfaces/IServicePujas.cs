using proyecto.Application.DTOs;
using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Interfaces
{
    public interface IServicePujas
    {
        Task<ICollection<PujasDTO>> ListAsync();
        Task<PujasDTO?> FindByIdAsync(int id);
        Task<int> AddAsync(Pujas entity);
        Task DeleteAsync(int id);

        Task<ICollection<PujasDTO>> FindByIDSubastaAsync(int subastaId);
    }
}
