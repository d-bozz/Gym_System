using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class SocioRepositorio : ISocioRepositorio
    {
            private readonly GymSystemContext _dbContext;

            public SocioRepositorio(GymSystemContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IQueryable<Socio>> Consultar(Expression<Func<Socio, bool>> filtro = null)
            {
                IQueryable<Socio> queryEntidad = filtro == null ? _dbContext.Socios : _dbContext.Socios.Where(filtro);
                return queryEntidad;
            }

            public async Task<Socio> Crear(Socio entidad)
            {
                try
                {
                    _dbContext.Set<Socio>().Add(entidad);
                    await _dbContext.SaveChangesAsync();
                    return entidad;
                }
                catch
                {
                    throw;
                }
            }

            public async Task<bool> Editar(Socio entidad)
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

            public async Task<bool> Eliminar(Socio entidad)
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

            public async Task<List<Socio>> Lista()
            {
                try
                {
                    return await _dbContext.Socios.ToListAsync();
                }
                catch
                {
                    throw;
                }
            }

            public async Task<Socio> Obtener(Expression<Func<Socio, bool>> filtro = null)
            {
                try
                {
                    return await _dbContext.Socios.Where(filtro).FirstOrDefaultAsync();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
