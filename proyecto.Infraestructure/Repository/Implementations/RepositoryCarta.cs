using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyecto.Infraestructure.Data;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace proyecto.Infraestructure.Repository.Implementations
{
    public class RepositoryCarta : IRepositoryCarta
    {
        private readonly SubastasPokemonDbContext _context;

        public RepositoryCarta(SubastasPokemonDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Cartas>> ListAsync()
        {
            return await _context.Cartas
                .Include(c => c.EstadoCarta)
                .Include(c => c.Vendedor)
                .Include(c => c.CartaCategoria)
                    .ThenInclude(cc => cc.Categoria)
                .Include(c => c.ImagenesCarta)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cartas?> FindByIdAsync(int id)
        {
            return await _context.Cartas
                .Include(c => c.EstadoCarta)
                .Include(c => c.Vendedor)
                .Include(c => c.CartaCategoria)
                    .ThenInclude(cc => cc.Categoria)
                .Include(c => c.ImagenesCarta)
                .Include(c => c.Subastas)
                .FirstOrDefaultAsync(c => c.CartaId == id);
        }

        public async Task<int> AddAsync(Cartas entity)
        {
            _context.Cartas.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cartas entity)
        {
            _context.Cartas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var carta = await _context.Cartas.FindAsync(id);
            if (carta != null)
            {
                _context.Cartas.Remove(carta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Cartas>> FindByNameAsync(string nombre)
        {
            return await _context.Cartas
                .Where(c => c.NombreCarta.Contains(nombre))
                .Include(c => c.EstadoCarta)
                .Include(c => c.Vendedor)
                .Include(c => c.CartaCategoria)
                    .ThenInclude(cc => cc.Categoria)
                .ToListAsync();
        }

        public async Task<ICollection<Cartas>> GetCartasByCategoria(int categoriaId)
        {
            return await _context.Cartas
                .Where(c => c.CartaCategoria.Any(cc => cc.CategoriaId == categoriaId))
                .Include(c => c.EstadoCarta)
                .Include(c => c.Vendedor)
                .Include(c => c.CartaCategoria)
                    .ThenInclude(cc => cc.Categoria)
                .ToListAsync();
        }

        public async Task<ICollection<Cartas>> GetCartasByEstado(int estadoId)
        {
            return await _context.Cartas
                .Where(c => c.EstadoCartaId == estadoId)
                .Include(c => c.EstadoCarta)
                .Include(c => c.Vendedor)
                .Include(c => c.CartaCategoria)
                    .ThenInclude(cc => cc.Categoria)
                .ToListAsync();
        }
    }
}