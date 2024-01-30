using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class HorarioActividadRepositorio : IHorarioActividadRepositorio
    {
        #region Properties and Fields
        private readonly GymSystemContext _dbContext;

        public HorarioActividadRepositorio(GymSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public async Task<IQueryable<HorarioActividad>> Consultar(Expression<Func<HorarioActividad, bool>> filtro = null)
        {
            IQueryable<HorarioActividad> queryEntidad = filtro == null ? _dbContext.HorarioActividads : _dbContext.HorarioActividads.Where(filtro);
            return queryEntidad;
        }

        public async Task<HorarioActividad> Crear(HorarioActividad entidad)
        {
            try
            {
                // Verificar si ya existe una actividad en el mismo horario
                bool existeActividadEnHorario = await _dbContext.HorarioActividads
                    .AnyAsync(h =>
                        h.DiaSemana == entidad.DiaSemana &&
                        ((h.HoraInicio >= entidad.HoraInicio && h.HoraInicio < entidad.HoraFin) ||
                         (h.HoraFin > entidad.HoraInicio && h.HoraFin <= entidad.HoraFin) ||
                         (h.HoraInicio <= entidad.HoraInicio && h.HoraFin >= entidad.HoraFin))
                    );

                if (existeActividadEnHorario)
                {
                    throw new InvalidOperationException("Ya existe una actividad en el mismo horario");
                }

                _dbContext.Set<HorarioActividad>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw new InvalidOperationException("Ya existe una actividad en el mismo horario");
            }
        }

        public async Task<bool> Editar(HorarioActividad entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(HorarioActividad entidad)
        {
            try
            {
                _dbContext.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<HorarioActividad>> Lista()
        {
            try
            {
                return await _dbContext.HorarioActividads.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<HorarioActividad> Obtener(Expression<Func<HorarioActividad, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.HorarioActividads.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
