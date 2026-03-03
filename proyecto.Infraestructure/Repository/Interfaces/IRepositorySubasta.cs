using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Infraestructure.Repository.Interfaces
{
    public interface IRepositorySubasta
    {
        Task<ICollection<Subastas>> ListAsync();
        Task<Subastas> FindByIdAsync(int id);
    }
}

