using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly GymSystemContext _dbContext;

        public RolRepositorio(GymSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Rol>> Lista()
        {
            try
            {
                return await _dbContext.Rols.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
