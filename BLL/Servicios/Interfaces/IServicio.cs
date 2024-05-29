using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    // Esta interfaz define los métodos para un servicio de Servicio.
    public interface IServicio
    {
        // ObtenerTodos() debe devolver todos los servicios de la base de datos.
        Task<IEnumerable<ServicioDto>> ObtenerTodos();

        // Agregar() debe añadir un nuevo servicio a la base de datos y devolver el servicio añadido.
        Task<ServicioDto> Agregar(ServicioDto modeloDto);

        // Actualizar() debe actualizar un servicio en la base de datos.
        Task Actualizar(ServicioDto modeloDto);

        // Remover() debe eliminar un servicio de la base de datos.
        Task Remover(int id);
    }

}
