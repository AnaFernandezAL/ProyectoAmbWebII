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
            _context.Usuarios.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuarios entity)
        {
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
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