using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.Interfaces.IRepositorio;

namespace Models.Repositorio
{
    // Esta clase es un repositorio genérico que puede trabajar con cualquier tipo de entidad.
    public class Repositorio<T> : IRepositorioGenerico<T> where T : class
    {
        // _context es una instancia del contexto de la base de datos.
        private readonly DogWalkPlusContext _context;
        // _dbSet es una referencia a la tabla correspondiente a la entidad T en la base de datos.
        private DbSet<T> _dbSet;

        // En el constructor, se inicializa el contexto de la base de datos y se establece _dbSet.
        public Repositorio(DogWalkPlusContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Agregar() añade una nueva entidad a la tabla correspondiente en la base de datos.
        public async Task Agregar(T entidad)
        {
            await _dbSet.AddAsync(entidad);
        }

        // ObtenerPrimero() devuelve la primera entidad que cumple con el filtro especificado.
        // También puede incluir propiedades relacionadas en la entidad devuelta.
        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro, string incluirPropiedades)
        {
            IQueryable<T> query = _dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        // ObtenerTodos() devuelve todas las entidades que cumplen con el filtro especificado.
        // También puede ordenar las entidades y/o incluir propiedades relacionadas.
        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string incluirPropiedades)
        {
            IQueryable<T> query = _dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        // Remover() elimina una entidad de la tabla correspondiente en la base de datos.
        public void Remover(T entidad)
        {
            _dbSet.Remove(entidad);
        }
    }

}
