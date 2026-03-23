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

        public async Task<ICollection<Usuarios>> ListAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Usuarios?> FindByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task<int> AddAsync(Usuarios entity)
        {
            await _context.Usuarios.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.UsuarioId;
        }

        public async Task UpdateAsync(Usuarios entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Eliminación lógica: marcar como bloqueado
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.EstadoUsuarioId = 2; // Bloqueado
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Usuarios>> FindByNameAsync(string nombre)
        {
            return await _context.Usuarios
                .Where(u => u.NombreCompleto.Contains(nombre))
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .ToListAsync();
        }

        public async Task<ICollection<Usuarios>> GetUsuariosByRol(int rolId)
        {
            return await _context.Usuarios
                .Where(u => u.RolId == rolId)
                .Include(u => u.Rol)
                .Include(u => u.EstadoUsuario)
                .ToListAsync();
        }
    }
}