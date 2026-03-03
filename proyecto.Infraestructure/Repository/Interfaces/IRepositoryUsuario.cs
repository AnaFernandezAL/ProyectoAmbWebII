using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<ICollection<Usuarios>> ListAsync();

        Task<Usuarios?> FindByIdAsync(int id);
        Task<int> AddAsync(Usuarios entity);
        Task UpdateAsync(Usuarios entity);
        Task DeleteAsync(int id);

        // Consultas adicionales (ejemplo)
        Task<ICollection<Usuarios>> FindByNameAsync(string nombre);
        Task<ICollection<Usuarios>> GetUsuariosByRol(int rolId);
    }
}