using AutoMapper;
using Microsoft.EntityFrameworkCore;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Repository.Interfaces;
using proyecto.Infraestructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
        private readonly SubastasPokemonDbContext _context;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper, SubastasPokemonDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);

            foreach (var dto in collection)
            {
                dto.CantidadSubastasCreadas = await _context.Subastas.CountAsync(s => s.VendedorId == dto.UsuarioId);
                dto.CantidadPujasRealizadas = await _context.Pujas.CountAsync(p => p.CompradorId == dto.UsuarioId);
            }

            return collection;
        }

        public async Task<UsuarioDTO?> FindByIdAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) return null;

            var dto = _mapper.Map<UsuarioDTO>(entity);

            // 🔹 Campos calculados
            dto.CantidadSubastasCreadas = await _context.Subastas.CountAsync(s => s.VendedorId == id);
            dto.CantidadPujasRealizadas = await _context.Pujas.CountAsync(p => p.CompradorId == id);

            return dto;
        }

        public async Task<int> AddAsync(UsuarioDTO dto)
        {
            var entity = _mapper.Map<Usuarios>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, UsuarioDTO dto)
        {
            var entity = _mapper.Map<Usuarios>(dto);
            entity.UsuarioId = id;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<UsuarioDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);

            foreach (var dto in collection)
            {
                dto.CantidadSubastasCreadas = await _context.Subastas.CountAsync(s => s.VendedorId == dto.UsuarioId);
                dto.CantidadPujasRealizadas = await _context.Pujas.CountAsync(p => p.CompradorId == dto.UsuarioId);
            }

            return collection;
        }

        public async Task<ICollection<UsuarioDTO>> GetUsuariosByRol(int rolId)
        {
            var list = await _repository.GetUsuariosByRol(rolId);
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);

            foreach (var dto in collection)
            {
                dto.CantidadSubastasCreadas = await _context.Subastas.CountAsync(s => s.VendedorId == dto.UsuarioId);
                dto.CantidadPujasRealizadas = await _context.Pujas.CountAsync(p => p.CompradorId == dto.UsuarioId);
            }

            return collection;
        }
    }
}