﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Interfaces.IRepositorio
{
    public interface IServicioRepositorio : IRepositorioGenerico<Servicio>
    {
        void Actualizar(Servicio servicio);

    }
}
