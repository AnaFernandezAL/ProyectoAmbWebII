using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto.Infraestructure.Models;

namespace proyecto.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCarta
    {
        Task<ICollection<Cartas>> ListAsync();
        Task<Cartas?> FindByIdAsync(int id);
        Task<int> AddAsync(Cartas entity);
        Task UpdateAsync(Cartas entity);

        Task<ICollection<Cartas>> FindByNameAsync(string nombre);
        Task<ICollection<Cartas>> GetCartasByCategoria(int categoriaId);
        Task<ICollection<Cartas>> GetCartasByEstado(int estadoId);
    }
}

