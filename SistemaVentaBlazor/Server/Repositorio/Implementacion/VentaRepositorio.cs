using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Globalization;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class VentaRepositorio : IVentaRepositorio
    {
        #region Properties and Fields
        private readonly GymSystemContext _dbcontext;

        public VentaRepositorio(GymSystemContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods
        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta VentaGenerada = new Venta();

            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                int CantidadDigitos = 4;
                try
                {
                    foreach (DetalleVenta dv in entidad.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();


                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    entidad.NumeroDocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(entidad);
                    await _dbcontext.SaveChangesAsync();

                    VentaGenerada = entidad;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return VentaGenerada;
        }

        public async Task<List<Venta>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = _dbcontext.Venta;

            #region No se ingresan Fechas
            if (string.IsNullOrEmpty(fechaInicio) && string.IsNullOrEmpty(fechaFin))
            {
                return query
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToList();
            }
            #endregion

            #region Se ingresa solo Fecha Inicio
            if (!string.IsNullOrEmpty(fechaInicio) && string.IsNullOrEmpty(fechaFin))
            {
                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date
                    )
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToList();
            }
            #endregion

            #region Se ingresa solo Fecha Fin
            if (string.IsNullOrEmpty(fechaInicio) && !string.IsNullOrEmpty(fechaFin))
            {
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToList();
            }
            #endregion

            #region Se ingresan ambas Fechas
            if (buscarPor == "fecha")
            {
                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                )
                .Include(dv => dv.DetalleVenta)
                .ThenInclude(p => p.IdProductoNavigation)
                .ToList();
            }
            #endregion

            #region Se Busca por Numero de Documento
            else
            {
                return query.Where(v => v.NumeroDocumento == numeroVenta)
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToList();
            }
            #endregion
        }
        
        public async Task<List<DetalleVenta>> Reporte(string FechaInicio, string FechaFin)
        {
            #region No se ingresan Fechas
            if (string.IsNullOrEmpty(FechaInicio) && string.IsNullOrEmpty(FechaFin))
            {
                return await _dbcontext.DetalleVenta
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .ToListAsync();
            }
            #endregion

            #region Se ingresa solo Fecha Inicio
            if (!string.IsNullOrEmpty(FechaInicio) && string.IsNullOrEmpty(FechaFin))
            {
                DateTime inicio = string.IsNullOrEmpty(FechaInicio)
                    ? DateTime.MinValue
                    : DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));

                List<DetalleVenta> listaInicio = await _dbcontext.DetalleVenta
                 .Include(p => p.IdProductoNavigation)
                 .Include(v => v.IdVentaNavigation)
                 .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= inicio.Date)
                 .ToListAsync();

                return listaInicio;
            }
            #endregion

            #region Se ingresa solo Fecha Fin
            if (string.IsNullOrEmpty(FechaInicio) && !string.IsNullOrEmpty(FechaFin))
            {
                DateTime Fin = string.IsNullOrEmpty(FechaFin)
                               ? DateTime.MaxValue
                               : DateTime.ParseExact(FechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                List<DetalleVenta> listaFin = await _dbcontext.DetalleVenta
                 .Include(p => p.IdProductoNavigation)
                 .Include(v => v.IdVentaNavigation)
                 .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date <= Fin.Date)
                 .ToListAsync();

                return listaFin;
            }
            #endregion

            #region Se ingresan Ambas Fechas
            DateTime fech_Inicio = string.IsNullOrEmpty(FechaInicio)
                ? DateTime.MinValue
                : DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));

            DateTime fech_Fin = string.IsNullOrEmpty(FechaFin)
                ? DateTime.MaxValue
                : DateTime.ParseExact(FechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

            List<DetalleVenta> listaResumen = await _dbcontext.DetalleVenta
                .Include(p => p.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date)
                .ToListAsync();
            #endregion

            return listaResumen;
        }
        #endregion
    }
}
