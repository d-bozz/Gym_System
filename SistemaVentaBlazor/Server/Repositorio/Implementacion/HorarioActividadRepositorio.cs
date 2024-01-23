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
                _dbContext.Set<HorarioActividad>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
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
