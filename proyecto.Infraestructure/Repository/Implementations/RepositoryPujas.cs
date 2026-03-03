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
    public class RepositoryPujas : IRepositoryPujas
    {
        private readonly SubastasPokemonDbContext _context;

        public RepositoryPujas(SubastasPokemonDbContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(Pujas entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pujas?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Pujas>> FindByIDSubastaAsync(int subastaId)
        {
            return await _context.Pujas
                .Where(p => p.SubastaId == subastaId)
               .Include(p => p.Comprador)
               .Include(p => p.Subasta)
               .ThenInclude(s => s.Carta)
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<ICollection<Pujas>> ListAsync()
        {
            return await _context.Pujas
            .Include(p => p.Subasta)
            .ThenInclude(s => s.Carta)
            .Include(p => p.Comprador)
            .AsNoTracking() .ToListAsync();
        }
    }
}
