using AutoMapper;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyecto.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO?> FindByIdAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);
            return usuario is null ? null : _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var usuarios = await _repository.ListAsync();
            return _mapper.Map<ICollection<UsuarioDTO>>(usuarios);
        }
    }
}
