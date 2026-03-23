using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Infraestructure.Repository.Interfaces
{
    public interface IRepositorySubasta
    {
        Task<ICollection<Subastas>> ListAsync();

        Task<Subastas?> FindByIdAsync(int id);

        Task<int> AddAsync(Subastas entity);

        Task UpdateAsync(Subastas entity);

        Task DeleteAsync(int id);

        Task<ICollection<Subastas>> ListActivasAsync();
        Task<ICollection<Subastas>> ListFinalizadasAsync();
        Task<int> GetNextNumberOrden();
    }
}


