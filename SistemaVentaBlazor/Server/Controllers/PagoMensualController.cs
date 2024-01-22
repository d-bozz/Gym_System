using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using SistemaVentaBlazor.Shared;

namespace SistemaVentaBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoMensualController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPagoMensualRepositorio _PagoMensualRepositorio;
        public PagoMensualController(IPagoMensualRepositorio PagoMensualRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _PagoMensualRepositorio = PagoMensualRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<PagoMensualDTO>> _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>();

            try
            {
                List<PagoMensualDTO> ListaPagoMensuals = new List<PagoMensualDTO>();
                IQueryable<PagoMensual> query = await _PagoMensualRepositorio.Consultar();
                query = query.Include(r => r.IdSocioNavigation);

                ListaPagoMensuals = _mapper.Map<List<PagoMensualDTO>>(query.ToList());

                if (ListaPagoMensuals.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>() { status = true, msg = "ok", value = ListaPagoMensuals };
                else
                    _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("ListaPorSocio/{id:int}")]
        public async Task<IActionResult> ListaPorSocio(int id)
        {
            ResponseDTO<List<PagoMensualDTO>> _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>();
            try
            {
                List<PagoMensualDTO> ListaPagoMensuales = new List<PagoMensualDTO>();

                IQueryable<PagoMensual> query = await _PagoMensualRepositorio.Consultar();
                query = query.Include(r => r.IdSocioNavigation);
                query = query.Where(p => p.IdSocio == id);

                ListaPagoMensuales = _mapper.Map<List<PagoMensualDTO>>(query.ToList());

                if (ListaPagoMensuales.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>() { status = true, msg = "ok", value = ListaPagoMensuales };
                else
                    _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<PagoMensualDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] PagoMensualDTO request)
        {
            ResponseDTO<PagoMensualDTO> _ResponseDTO = new ResponseDTO<PagoMensualDTO>();
            try
            {
                PagoMensual _PagoMensual = _mapper.Map<PagoMensual>(request);

                PagoMensual _PagoMensualCreado = await _PagoMensualRepositorio.Crear(_PagoMensual);

                if (_PagoMensualCreado.IdPago != 0)
                    _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = true, msg = "ok", value = _mapper.Map<PagoMensualDTO>(_PagoMensualCreado) };
                else
                    _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = "No se pudo crear el PagoMensual" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] PagoMensualDTO request)
        {
            ResponseDTO<PagoMensualDTO> _ResponseDTO = new ResponseDTO<PagoMensualDTO>();
            try
            {
                PagoMensual _PagoMensual = _mapper.Map<PagoMensual>(request);
                PagoMensual _PagoMensualParaEditar = await _PagoMensualRepositorio.Obtener(u => u.IdPago == _PagoMensual.IdPago);

                if (_PagoMensualParaEditar != null)
                {
                    _PagoMensualParaEditar.Mes = _PagoMensual.Mes;
                    _PagoMensualParaEditar.Anio = _PagoMensual.Anio;
                    _PagoMensualParaEditar.FechaPago = _PagoMensual.FechaPago;
                    _PagoMensualParaEditar.Monto = _PagoMensual.Monto;

                    bool respuesta = await _PagoMensualRepositorio.Editar(_PagoMensualParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = true, msg = "ok", value = _mapper.Map<PagoMensualDTO>(_PagoMensualParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = "No se pudo editar el PagoMensual" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = "No se encontró el PagoMensual" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                PagoMensual _PagoMensualEliminar = await _PagoMensualRepositorio.Obtener(u => u.IdPago == id);

                if (_PagoMensualEliminar != null)
                {

                    bool respuesta = await _PagoMensualRepositorio.Eliminar(_PagoMensualEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el PagoMensual", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            ResponseDTO<PagoMensualDTO> _ResponseDTO = new ResponseDTO<PagoMensualDTO>();
            try
            {
                PagoMensual _PagoMensual = await _PagoMensualRepositorio.Obtener(u => u.IdPago == id);

                if (_PagoMensual != null)
                {
                    PagoMensualDTO pagoDTO = _mapper.Map<PagoMensualDTO>(_PagoMensual);
                    _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = true, msg = "ok", value = pagoDTO };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = "No se encontró el pago", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<PagoMensualDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

    }
}
