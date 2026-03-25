using AutoMapper;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Implementations
{
    public class ServiceSubasta : IServiceSubasta
    {
        private readonly IRepositorySubasta _repository;
        private readonly IMapper _mapper;
        private readonly SubastasPokemonDbContext _context;

        public ServiceSubasta(IRepositorySubasta repository, IMapper mapper, SubastasPokemonDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<SubastaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<SubastaDTO>>(list);

            foreach (var dto in collection)
            {
                dto.CantidadPujas = await _context.Pujas.CountAsync(p => p.SubastaId == dto.SubastaId);
            }

            return collection;
        }

        public async Task<SubastaDTO?> FindByIdAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) return null;

            var dto = _mapper.Map<SubastaDTO>(entity);

            // Campo calculado para pujas
            dto.CantidadPujas = await _context.Pujas.CountAsync(p => p.SubastaId == id);

            return dto;
        }

        public async Task<SubastaDTO?> AddAsync(SubastaDTO dto)
        {
            var entity = _mapper.Map<Subastas>(dto);
            await _repository.AddAsync(entity);

            var subastaGuardada = await _repository.FindByIdAsync(entity.SubastaId);

            if (subastaGuardada == null) return null;

            var dtoResult = _mapper.Map<SubastaDTO>(subastaGuardada);
            return dtoResult;
        }


        public async Task UpdateAsync(int id, SubastaDTO dto)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null) throw new Exception("Subasta no encontrada");

            if (entity.FechaInicio <= DateTime.Now)
                throw new Exception("No se puede editar una subasta que ya inició");

            if (entity.Pujas != null && entity.Pujas.Count > 0)
                throw new Exception("No se puede editar una subasta con pujas");

            entity.FechaInicio = dto.FechaInicio;
            entity.FechaCierre = dto.FechaCierre;
            entity.PrecioBase = dto.PrecioBase;
            entity.IncrementoMinimo = dto.IncrementoMinimo;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<SubastaDTO>> ListActivasAsync()
        {
            var list = await _repository.ListActivasAsync();
            var collection = _mapper.Map<ICollection<SubastaDTO>>(list);

            foreach (var dto in collection)
            {
                dto.CantidadPujas = await _context.Pujas.CountAsync(p => p.SubastaId == dto.SubastaId);
            }

            return collection;
        }

        public async Task<ICollection<SubastaDTO>> ListFinalizadasAsync()
        {
            var list = await _repository.ListFinalizadasAsync();
            var collection = _mapper.Map<ICollection<SubastaDTO>>(list);

            foreach (var dto in collection)
            {
                dto.CantidadPujas = await _context.Pujas.CountAsync(p => p.SubastaId == dto.SubastaId);
            }

            return collection;
        }

        public async Task<int> GetNextNumberOrden()
        {
            int nextReceipt = await _repository.GetNextNumberOrden();
            return nextReceipt + 1;
        }
    }
}