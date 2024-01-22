using SistemaVentaBlazor.Server.Models;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Contrato
{
    public interface IPagoMensualRepositorio
    {
        Task<List<PagoMensual>> Lista();
        Task<List<PagoMensual>> ListaPorSocio(Socio entidad);
        Task<PagoMensual> Obtener(Expression<Func<PagoMensual, bool>> filtro = null);
        Task<PagoMensual> Crear(PagoMensual entidad);
        Task<bool> Editar(PagoMensual entidad);
        Task<bool> Eliminar(PagoMensual entidad);
        Task<IQueryable<PagoMensual>> Consultar(Expression<Func<PagoMensual, bool>> filtro = null);
    }
}
