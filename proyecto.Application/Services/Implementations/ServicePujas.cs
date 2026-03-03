using AutoMapper;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Data;
using proyecto.Infraestructure.Models;
using proyecto.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Implementations
{
    public class ServicePujas : IServicePujas
    {
        private readonly IRepositoryPujas _repository; 
        private readonly IMapper _mapper;

        public ServicePujas(IRepositoryPujas repository, IMapper mapper) 
        { 
            _repository = repository; 
            _mapper = mapper; 
        }

        public Task<int> AddAsync(Pujas entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PujasDTO?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PujasDTO>> FindByIDSubastaAsync(int subastaId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PujasDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<PujasDTO>>(list);
            return collection;
        }
    }
}
