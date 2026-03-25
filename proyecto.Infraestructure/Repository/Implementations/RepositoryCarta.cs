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
                    .ThenInclude(s => s.EstadoSubasta)
                .FirstOrDefaultAsync(c => c.CartaId == id);
        }

        public async Task<int> AddAsync(Cartas entity)
        {
            await ApplyCategoriasAsync(entity, entity.CartaCategoria?.Select(cc => cc.CategoriaId).ToArray());

            _context.Cartas.Add(entity);
            await _context.SaveChangesAsync();
            return entity.CartaId;
        }
        public async Task UpdateAsync(Cartas entity)
        {
            var carta = await _context.Cartas
                .Include(c => c.CartaCategoria)
                .Include(c => c.ImagenesCarta)
                .FirstOrDefaultAsync(c => c.CartaId == entity.CartaId);

            if (carta == null) throw new Exception("Carta no encontrada");

            if (entity.EstadoCartaId > 0)
            {
                carta.EstadoCartaId = entity.EstadoCartaId;
            }

            carta.NombreCarta = entity.NombreCarta;
            carta.Descripcion = entity.Descripcion;
            carta.Condicion = entity.Condicion;
            carta.Edicion = entity.Edicion;
            carta.Rareza = entity.Rareza;

            await ApplyCategoriasAsync(carta, entity.CartaCategoria?.Select(cc => cc.CategoriaId).ToArray());

            if (entity.ImagenesCarta != null && entity.ImagenesCarta.Any())
            {
                _context.ImagenesCarta.RemoveRange(carta.ImagenesCarta);

                carta.ImagenesCarta = new List<ImagenesCarta>();
                foreach (var img in entity.ImagenesCarta)
                {
                    carta.ImagenesCarta.Add(new ImagenesCarta
                    {
                        Urlimagen = img.Urlimagen,
                        EsPrincipal = img.EsPrincipal,
                        CartaId = carta.CartaId
                    });
                }
            }
            else
            {
                if (entity.ImagenesCarta != null && entity.ImagenesCarta.Any())
                {
                    foreach (var img in carta.ImagenesCarta)
                    {
                        img.EsPrincipal = false;
                    }

                    foreach (var imgDto in entity.ImagenesCarta)
                    {
                        var img = carta.ImagenesCarta.FirstOrDefault(x => x.Urlimagen == imgDto.Urlimagen);
                        if (img != null)
                        {
                            img.EsPrincipal = imgDto.EsPrincipal;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }


        private async Task ApplyCategoriasAsync(Cartas cartaToUpdate, int[]? selectedCategorias)
        {
            if (selectedCategorias == null || selectedCategorias.Length == 0)
            {
                cartaToUpdate.CartaCategoria = new List<CartaCategoria>();
                return;
            }

            var categorias = await _context.Categorias
                .Where(c => selectedCategorias.Contains(c.CategoriaId))
                .ToListAsync();

            cartaToUpdate.CartaCategoria = categorias
                .Select(c => new CartaCategoria
                {
                    CartaId = cartaToUpdate.CartaId,
                    CategoriaId = c.CategoriaId
                })
                .ToList();
        }

        private async Task ApplyImagenesAsync(Cartas cartaToUpdate, ICollection<ImagenesCarta>? nuevasImagenes)
        {
            if (nuevasImagenes == null || !nuevasImagenes.Any())
            {
                return;
            }

            var existentes = await _context.ImagenesCarta
                .Where(i => i.CartaId == cartaToUpdate.CartaId)
                .ToListAsync();

            var idsNuevos = nuevasImagenes.Select(i => i.ImagenId).ToHashSet();

            var aEliminar = existentes.Where(e => !idsNuevos.Contains(e.ImagenId)).ToList();
            _context.ImagenesCarta.RemoveRange(aEliminar);

            var nuevasSinId = nuevasImagenes.Where(i => i.ImagenId == 0).ToList();
            foreach (var img in nuevasSinId)
            {
                cartaToUpdate.ImagenesCarta.Add(new ImagenesCarta
                {
                    CartaId = cartaToUpdate.CartaId,
                    Urlimagen = img.Urlimagen,
                    EsPrincipal = img.EsPrincipal
                });
            }

            foreach (var existente in existentes)
            {
                var nueva = nuevasImagenes.FirstOrDefault(i => i.ImagenId == existente.ImagenId);
                if (nueva != null)
                {
                    existente.Urlimagen = nueva.Urlimagen;
                    existente.EsPrincipal = nueva.EsPrincipal;
                }
            }
        }

        public async Task<ICollection<Cartas>> FindByNameAsync(string nombre)
        {
            return await _context.Cartas
                .Where(c => c.NombreCarta.ToLower().Contains(nombre.ToLower())
                 && c.EstadoCartaId == 1)
                .Include(c => c.EstadoCarta)
                .Include(c => c.Vendedor)
                .Include(c => c.CartaCategoria)
                    .ThenInclude(cc => cc.Categoria)
                .Include(c => c.ImagenesCarta)
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
