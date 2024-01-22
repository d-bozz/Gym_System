using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class PagoMensualRepositorio : IPagoMensualRepositorio
    {
        #region Properties and Fields
        private readonly GymSystemContext _dbContext;

        public PagoMensualRepositorio(GymSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public async Task<IQueryable<PagoMensual>> Consultar(Expression<Func<PagoMensual, bool>> filtro = null)
        {
            IQueryable<PagoMensual> queryEntidad = filtro == null ? _dbContext.PagoMensuales : _dbContext.PagoMensuales.Where(filtro);
            return queryEntidad;
        }

        public async Task<PagoMensual> Crear(PagoMensual entidad)
        {
            try
            {
                _dbContext.Set<PagoMensual>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(PagoMensual entidad)
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

        public async Task<bool> Eliminar(PagoMensual entidad)
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

        public async Task<List<PagoMensual>> Lista()
        {
            try
            {
                return await _dbContext.PagoMensuales.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PagoMensual>> ListaPorSocio(Socio entidad)
        {
            try
            {
                return await _dbContext.PagoMensuales
                    .Where(p => p.IdSocio == entidad.IdSocio)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<PagoMensual> Obtener(Expression<Func<PagoMensual, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.PagoMensuales.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
