using AutoMapper;
using Microsoft.EntityFrameworkCore;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Data;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Repository.Implementations;
using proyecto.Infraestructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Implementations
{
    public class ServiceCarta : IServiceCarta
    {
        private readonly IRepositoryCarta _repository;
        private readonly IMapper _mapper;
        private readonly SubastasPokemonDbContext _context;

        public ServiceCarta(IRepositoryCarta repository, IMapper mapper, SubastasPokemonDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<CartaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<CartaDTO>>(list);

            return collection;
        }

        public async Task<CartaDTO?> FindByIdAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) return null;

            var dto = _mapper.Map<CartaDTO>(entity);

            return dto;
        }

        public async Task<int> AddAsync(CartaDTO dto)
        {
            var entity = _mapper.Map<Cartas>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, CartaDTO dto)
        {
            var entity = _mapper.Map<Cartas>(dto);
            entity.CartaId = id;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<CartaDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);
            var collection = _mapper.Map<ICollection<CartaDTO>>(list);
            return collection;
        }

        public async Task<ICollection<CartaDTO>> GetCartasByCategoria(int categoriaId)
        {
            var list = await _repository.GetCartasByCategoria(categoriaId);
            var collection = _mapper.Map<ICollection<CartaDTO>>(list);
            return collection;
        }

        public async Task<ICollection<CartaDTO>> GetCartasByEstado(int estadoId)
        {
            var list = await _repository.GetCartasByEstado(estadoId);
            var collection = _mapper.Map<ICollection<CartaDTO>>(list);
            return collection;
        }
    }
}
