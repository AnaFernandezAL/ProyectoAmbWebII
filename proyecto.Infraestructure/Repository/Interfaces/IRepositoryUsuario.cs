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
        // Listado de usuarios
        Task<ICollection<Usuarios>> ListAsync();

        // Detalle de usuario
        Task<Usuarios?> FindByIdAsync(int id);

        // Crear usuario
        Task<int> AddAsync(Usuarios entity);

        // Actualizar usuario (perfil, estado, etc.)
        Task UpdateAsync(Usuarios entity);

        // Eliminación lógica (bloqueo)
        Task DeleteAsync(int id);

        // Buscar por nombre
        Task<ICollection<Usuarios>> FindByNameAsync(string nombre);

        // Filtrar por rol
        Task<ICollection<Usuarios>> GetUsuariosByRol(int rolId);
    }
}
