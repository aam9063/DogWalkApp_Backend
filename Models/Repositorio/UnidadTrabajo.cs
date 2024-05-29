using Models.Context;
using Models.Interfaces.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repositorio
{
    // Esta clase implementa la interfaz IUnidadTrabajo, que define los métodos para una Unidad de Trabajo.
    public class UnidadTrabajo : IUnidadTrabajo
    {
        // _context es una instancia del contexto de la base de datos.
        private readonly DogWalkPlusContext _context;
        // Servicio es una instancia del repositorio de Servicio, que proporciona métodos para interactuar con la tabla de Servicios en la base de datos.
        public IServicioRepositorio Servicio { get; private set; }

        // En el constructor, se inicializa el contexto de la base de datos y se crea una nueva instancia del repositorio de Servicio.
        public UnidadTrabajo(DogWalkPlusContext context)
        {
            _context = context;
            Servicio = new ServicioRepositorio(context);
        }

        // Dispose() libera los recursos utilizados por el contexto de la base de datos.
        public void Dispose()
        {
            _context.Dispose();
        }

        // Guardar() guarda todos los cambios realizados en el contexto de la base de datos en la base de datos.
        public async Task Guardar()
        {
            await _context.SaveChangesAsync();
        }
    }


}
