using Models.Context;
using Models.Interfaces.IRepositorio;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repositorio
{
    // Esta clase es un repositorio específico para la entidad Servicio.
    public class ServicioRepositorio : Repositorio<Servicio>, IServicioRepositorio
    {
        // _context es una instancia del contexto de la base de datos.
        private readonly DogWalkPlusContext _context;

        // En el constructor, se inicializa el contexto de la base de datos.
        public ServicioRepositorio(DogWalkPlusContext context) : base(context)
        {
            _context = context;
        }

        // Actualizar() busca un servicio en la base de datos por su ID y actualiza sus propiedades.
        public void Actualizar(Servicio servicio)
        {
            // Busca el servicio en la base de datos.
            var servicioDb = _context.Servicios.FirstOrDefault(s => s.IdServicio == servicio.IdServicio);
            // Si el servicio existe en la base de datos, actualiza sus propiedades.
            if (servicioDb != null)
            {
                servicioDb.NombreServicio = servicio.NombreServicio;
                servicioDb.DescripcionServicio = servicio.DescripcionServicio;
                // Guarda los cambios en la base de datos.
                _context.SaveChanges();
            }
        }
    }


}
