using SistemaVentaBlazor.Server.Models;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Contrato
{
    public interface ISocioRepositorio
    {
        Task<List<Socio>> Lista();
        Task<Socio> Obtener(Expression<Func<Socio, bool>> filtro = null);
        Task<Socio> Crear(Socio entidad);
        Task<bool> Editar(Socio entidad);
        Task<bool> Eliminar(Socio entidad);
        Task<IQueryable<Socio>> Consultar(Expression<Func<Socio, bool>> filtro = null);
    }
}
