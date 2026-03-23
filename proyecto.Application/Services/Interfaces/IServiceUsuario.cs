using proyecto.Application.DTOs;
using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Interfaces
{
    public interface IServiceUsuario
    {
        // Listado de usuarios
        Task<ICollection<UsuarioDTO>> ListAsync();

        // Detalle de usuario
        Task<UsuarioDTO?> FindByIdAsync(int id);

        // Crear usuario
        Task<int> AddAsync(UsuarioDTO dto);

        // Actualización genérica (no recomendada para perfil)
        Task UpdateAsync(int id, UsuarioDTO dto);

        // Eliminación lógica (bloqueo)
        Task DeleteAsync(int id);

        // Buscar por nombre
        Task<ICollection<UsuarioDTO>> FindByNameAsync(string nombre);

        // Filtrar por rol
        Task<ICollection<UsuarioDTO>> GetUsuariosByRol(int rolId);

        // ✅ Actualización de perfil (solo Nombre y Correo)
        Task UpdatePerfilAsync(int id, UsuarioDTO dto);

        // ✅ Bloqueo / Activación
        Task CambiarEstadoAsync(int id, int nuevoEstadoId);
    }
}