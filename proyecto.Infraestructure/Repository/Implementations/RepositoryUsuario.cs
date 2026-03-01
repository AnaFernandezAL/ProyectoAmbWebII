using proyecto.Infraestructure.Data;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Infraestructure.Repository.Implementations
{
    public class RepositoryUsuario : IRepositoryUsuario 
    { 
        private readonly SubastasPokemonDbContext _context; 
        public RepositoryUsuario(SubastasPokemonDbContext context) 
        { 
            _context = context; 
        } 
        public async Task<Usuarios> FindByIdAsync(int id) 
        { 
            throw new NotImplementedException();
        } 
        public async Task<ICollection<Usuarios>> ListAsync() 
        { 
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .AsNoTracking()
                .ToListAsync(); 
        } 
    }
}
