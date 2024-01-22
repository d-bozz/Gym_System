using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Globalization;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class DashBoardRepositorio : IDashBoardRepositorio
    {
        #region Properties and Fields
        private readonly GymSystemContext _dbcontext;
        public DashBoardRepositorio(GymSystemContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Estadisticas de Socios
        public async Task<int> TotalSocios()
        {
            try
            {
                IQueryable<Socio> query = _dbcontext.Socios;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> TotalSociosActivos()
        {
            try
            {
                IQueryable<Socio> query = _dbcontext.Socios.Where(socio => socio.Activo == true);
                int total = await query.CountAsync();
                return total;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> NuevosSocios()
        {
            try
            {
                DateTime fechaInicioMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                IQueryable<Socio> query = _dbcontext.Socios
                    .Where(socio => socio.FechaInicioMembresia >= fechaInicioMesActual);

                int totalNuevosSocios = await query.CountAsync();
                return totalNuevosSocios;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> PagosPendientes()
        {
            try
            {
                DateTime fechaActual = DateTime.Now;
                int mesActual = fechaActual.Month;
                int anioActual = fechaActual.Year;

                IQueryable<Socio> sociosActivos = _dbcontext.Socios.Where(socio => socio.Activo == true);

                int totalSociosActivos = await sociosActivos.CountAsync();

                IQueryable<PagoMensual> pagosRegistrados = _dbcontext.PagoMensuales
                    .Where(pago => pago.Mes == mesActual && pago.Anio == anioActual);

                int totalPagosRegistrados = await pagosRegistrados.CountAsync();

                int pagosPendientes = totalSociosActivos - totalPagosRegistrados;

                return pagosPendientes;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Estadisticas por Ventas
        public async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            try
            {
                IQueryable<Venta> _ventaQuery = _dbcontext.Venta.AsQueryable();

                if (_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _dbcontext.Venta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

                    ultimaFecha = ultimaFecha.Value.AddDays(-7);

                    IQueryable<Venta> query = _dbcontext.Venta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
                    total = query.Count();
                }

                return total;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Venta> _ventaQuery = _dbcontext.Venta.AsQueryable();

                if (_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _dbcontext.Venta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
                    ultimaFecha = ultimaFecha.Value.AddDays(-7);
                    IQueryable<Venta> query = _dbcontext.Venta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);

                    resultado = query
                         .Select(v => v.Total)
                         .Sum(v => v.Value);
                }


                return Convert.ToString(resultado, new CultureInfo("es-PE"));
            }
            catch
            {
                throw;
            }


        }
        public async Task<int> TotalProductos()
        {
            try
            {
                IQueryable<Producto> query = _dbcontext.Productos;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            try
            {
                IQueryable<Venta> _ventaQuery = _dbcontext.Venta.AsQueryable();
                if (_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _dbcontext.Venta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
                    ultimaFecha = ultimaFecha.Value.AddDays(-7);

                    IQueryable<Venta> query = _dbcontext.Venta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);

                    resultado = query
                        .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                        .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                        .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
                }


                return resultado;

            }
            catch
            {
                throw;
            }

        }
        #endregion
    }
}
