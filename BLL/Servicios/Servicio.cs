using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Servicios.Interfaces;
using Models.DTOs;
using Models.Interfaces.IRepositorio;

namespace BLL.Servicios
{
    // Esta clase implementa la interfaz IServicio, que define los métodos para un servicio de Servicio.
    public class Servicio : IServicio
    {
        // _unidadTrabajo es una instancia de la Unidad de Trabajo, que agrupa varias operaciones en una sola transacción.
        private readonly IUnidadTrabajo _unidadTrabajo;
        // _mapper es una instancia de AutoMapper, que se utiliza para mapear objetos de un tipo a otro.
        private readonly IMapper _mapper;

        // En el constructor, se inicializan la Unidad de Trabajo y AutoMapper.
        public Servicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        // Estas son las propiedades de la clase Servicio.
        public string NombreServicio { get; private set; }
        public string DescripcionServicio { get; private set; }

        // Actualizar() actualiza un servicio en la base de datos.
        public async Task Actualizar(ServicioDto modeloDto)
        {
            try
            {
                var servicioDb = await _unidadTrabajo.Servicio.ObtenerPrimero(e => e.NombreServicio == modeloDto.NombreServicio);
                if (servicioDb != null)
                    throw new TaskCanceledException("El servicio ya existe");

                servicioDb.NombreServicio = modeloDto.NombreServicio;
                servicioDb.DescripcionServicio = modeloDto.DescripcionServicio;
                _unidadTrabajo.Servicio.Actualizar(servicioDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Agregar() añade un nuevo servicio a la base de datos.
        public async Task<ServicioDto> Agregar(ServicioDto modeloDto)
        {
            try
            {
                Models.Models.Servicio servicio = new Models.Models.Servicio
                {
                    NombreServicio = modeloDto.NombreServicio,
                    DescripcionServicio = modeloDto.DescripcionServicio
                };
                await _unidadTrabajo.Servicio.Agregar(servicio);
                await _unidadTrabajo.Guardar();

                return _mapper.Map<ServicioDto>(servicio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // ObtenerTodos() devuelve todos los servicios de la base de datos.
        public async Task<IEnumerable<ServicioDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Servicio.ObtenerTodos(
                                            orderBy: e => e.OrderBy(e => e.NombreServicio));
                return _mapper.Map<IEnumerable<ServicioDto>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Remover() elimina un servicio de la base de datos.
        public async Task Remover(int id)
        {
            try
            {
                var servicioDb = await _unidadTrabajo.Servicio.ObtenerPrimero(e => e.NombreServicio == NombreServicio);
                if (servicioDb == null)
                    throw new TaskCanceledException("El servicio no existe");
                _unidadTrabajo.Servicio.Remover(servicioDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
