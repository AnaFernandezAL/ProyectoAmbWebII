using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto.Infraestructure.Data;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Repository.Interfaces;

namespace proyecto.Infraestructure.Repository.Implementations
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        private readonly SubastasPokemonDbContext _context;

        public RepositoryCategoria(SubastasPokemonDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Categorias>> ListAsync()
        {
            return await _context.Categorias
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categorias?> FindByIdAsync(int id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<int> AddAsync(Categorias entity)
        {
            _context.Categorias.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categorias entity)
        {
            _context.Categorias.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}


