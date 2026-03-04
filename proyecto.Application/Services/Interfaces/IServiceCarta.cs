using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto.Application.DTOs;

namespace proyecto.Application.Services.Interfaces
{
    public interface IServiceCarta
    {
        Task<ICollection<CartaDTO>> ListAsync();
        Task<CartaDTO?> FindByIdAsync(int id);
        Task<int> AddAsync(CartaDTO dto);
        Task UpdateAsync(int id, CartaDTO dto);
        Task DeleteAsync(int id);

        Task<ICollection<CartaDTO>> FindByNameAsync(string nombre);
        Task<ICollection<CartaDTO>> GetCartasByCategoria(int categoriaId);
        Task<ICollection<CartaDTO>> GetCartasByEstado(int estadoId);
    }
}

