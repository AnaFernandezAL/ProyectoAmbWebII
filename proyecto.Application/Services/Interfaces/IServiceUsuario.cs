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
        Task<ICollection<UsuarioDTO>> ListAsync();
        Task<UsuarioDTO?> FindByIdAsync(int id);
        Task<int> AddAsync(UsuarioDTO dto);
        Task UpdateAsync(int id, UsuarioDTO dto);
        Task DeleteAsync(int id);
        Task<ICollection<UsuarioDTO>> FindByNameAsync(string nombre);
        Task<ICollection<UsuarioDTO>> GetUsuariosByRol(int rolId);
    }
}

