using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;


namespace BLL.Servicios.Interfaces
{
    public interface IPaseadorServicio
    {
        Task<IEnumerable<PaseadorDto>> ObtenerTodos();
        Task<PaseadorDto> Agregar(PaseadorDto modeloDto);
        Task Actualizar(PaseadorDto modeloDto);
        Task Remover(int id);
    }
}
