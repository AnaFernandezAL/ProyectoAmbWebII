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
    public class ServiceCategoria : IServiceCategoria
    {
        private readonly IRepositoryCategoria _repository;
        private readonly IMapper _mapper;

        public ServiceCategoria(IRepositoryCategoria repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoriaDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            return _mapper.Map<ICollection<CategoriaDTO>>(list);
        }

        public async Task<CategoriaDTO?> FindByIdAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);
            return _mapper.Map<CategoriaDTO?>(entity);
        }
    }

}
