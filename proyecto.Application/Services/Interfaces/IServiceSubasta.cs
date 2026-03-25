using proyecto.Application.DTOs;
using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Interfaces
{
    public interface IServiceSubasta
    {
        Task<ICollection<SubastaDTO>> ListAsync();
        Task<SubastaDTO?> FindByIdAsync(int id);
        Task<SubastaDTO?> AddAsync(SubastaDTO dto);

        Task UpdateAsync(int id, SubastaDTO dto);
        Task DeleteAsync(int id);
        Task<ICollection<SubastaDTO>> ListActivasAsync();
        Task<ICollection<SubastaDTO>> ListFinalizadasAsync();
        Task<int> GetNextNumberOrden();
    }

}


