using SistemaVentaBlazor.Server.Models;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Contrato
{
    public interface IHorarioActividadRepositorio
    {
        Task<List<HorarioActividad>> Lista();
        Task<HorarioActividad> Obtener(Expression<Func<HorarioActividad, bool>> filtro = null);
        Task<HorarioActividad> Crear(HorarioActividad entidad);
        Task<bool> Editar(HorarioActividad entidad);
        Task<bool> Eliminar(HorarioActividad entidad);
        Task<IQueryable<HorarioActividad>> Consultar(Expression<Func<HorarioActividad, bool>> filtro = null);
    }
}
