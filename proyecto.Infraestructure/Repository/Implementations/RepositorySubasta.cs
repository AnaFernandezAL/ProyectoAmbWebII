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

        public async Task<Subastas> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Subastas>> ListAsync()
        {
            // Select * from Subastas con relaciones
            return await _context.Subastas
                .Include(s => s.Carta)
                    .ThenInclude(c => c.ImagenesCarta)
                .Include(s => s.Pujas)
                .Include(s => s.EstadoSubasta)
                .Include(s => s.Vendedor)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

