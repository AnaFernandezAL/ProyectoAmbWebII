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
    public class RepositorySubasta : IRepositorySubasta
    {
        private readonly SubastasPokemonDbContext _context;

        public RepositorySubasta(SubastasPokemonDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Subastas>> ListAsync()
        {
            return await _context.Subastas
                .Include(s => s.Carta)
                    .ThenInclude(c => c.ImagenesCarta)
                .Include(s => s.Carta)
                    .ThenInclude(c => c.CartaCategoria)
                        .ThenInclude(cc => cc.Categoria)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Pujas)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Subastas?> FindByIdAsync(int id)
        {
            return await _context.Subastas
                .Include(s => s.Carta)
                    .ThenInclude(c => c.ImagenesCarta)
                .Include(s => s.Carta)
                    .ThenInclude(c => c.CartaCategoria)
                        .ThenInclude(cc => cc.Categoria)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Pujas)
                .FirstOrDefaultAsync(s => s.SubastaId == id);
        }

        public async Task<int> AddAsync(Subastas entity)
        {
            _context.Subastas.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subastas entity)
        {
            _context.Subastas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subasta = await _context.Subastas.FindAsync(id);
            if (subasta != null)
            {
                _context.Subastas.Remove(subasta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Subastas>> ListActivasAsync()
        {
            return await _context.Subastas
                .Where(s => s.EstadoSubasta.NombreEstado == "Abierta")
                .Include(s => s.Carta)
                    .ThenInclude(c => c.ImagenesCarta)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Pujas)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Subastas>> ListFinalizadasAsync()
        {
            return await _context.Subastas
                .Where(s => s.EstadoSubasta.NombreEstado == "Cerrada")
                .Include(s => s.Carta)
                    .ThenInclude(c => c.ImagenesCarta)
                .Include(s => s.Vendedor)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Pujas)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}