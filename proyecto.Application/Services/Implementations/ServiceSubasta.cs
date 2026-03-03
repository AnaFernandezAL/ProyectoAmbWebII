using AutoMapper;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Repository.Interfaces;
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

        public ServiceSubasta(IRepositorySubasta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SubastaDTO?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<SubastaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<SubastaDTO>>(list);
        }
    }
}

