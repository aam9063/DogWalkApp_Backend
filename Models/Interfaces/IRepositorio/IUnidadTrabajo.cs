using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Interfaces.IRepositorio
{
    public interface IUnidadTrabajo :IDisposable //Mecanismo para liberar memoria del sistema
    {
        IServicioRepositorio Servicio { get; }

        Task Guardar();
    }
}
