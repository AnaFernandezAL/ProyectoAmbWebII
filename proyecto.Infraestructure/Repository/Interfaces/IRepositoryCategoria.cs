using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto.Infraestructure.Models;

namespace proyecto.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCategoria
    {
        Task<ICollection<Categorias>> ListAsync();
        Task<Categorias?> FindByIdAsync(int id);
        Task<int> AddAsync(Categorias entity);
        Task UpdateAsync(Categorias entity);
        Task DeleteAsync(int id);
    }
}

