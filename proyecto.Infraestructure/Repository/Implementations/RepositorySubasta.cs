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
            var subasta = await _context.Subastas
                .Include(s => s.Pujas)
                .FirstOrDefaultAsync(s => s.SubastaId == entity.SubastaId);

            if (subasta == null)
                throw new Exception("Subasta no encontrada");

            if (subasta.FechaInicio <= DateTime.Now)
                throw new Exception("No se puede editar una subasta que ya inició");

            if (subasta.Pujas != null && subasta.Pujas.Count > 0)
                throw new Exception("No se puede editar una subasta con pujas");

            subasta.FechaInicio = entity.FechaInicio;
            subasta.FechaCierre = entity.FechaCierre;
            subasta.PrecioBase = entity.PrecioBase;
            subasta.IncrementoMinimo = entity.IncrementoMinimo;

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

        public async Task<int> GetNextNumberOrden()
        {
            int current = 0;

            string sql = string.Format("SELECT IDENT_CURRENT ('Subastas') AS Current_Identity;");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            current = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return await Task.FromResult(current);
        }
    }
}