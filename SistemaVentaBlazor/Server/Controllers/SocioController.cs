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
    public class SocioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISocioRepositorio _SocioRepositorio;
        public SocioController(ISocioRepositorio SocioRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _SocioRepositorio = SocioRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<SocioDTO>> _ResponseDTO = new ResponseDTO<List<SocioDTO>>();

            try
            {
                List<SocioDTO> ListaSocios = new List<SocioDTO>();
                IQueryable<Socio> query = await _SocioRepositorio.Consultar();

                ListaSocios = _mapper.Map<List<SocioDTO>>(query.ToList());

                if (ListaSocios.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<SocioDTO>>() { status = true, msg = "ok", value = ListaSocios };
                else
                    _ResponseDTO = new ResponseDTO<List<SocioDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<SocioDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] SocioDTO request)
        {
            ResponseDTO<SocioDTO> _ResponseDTO = new ResponseDTO<SocioDTO>();
            try
            {
                Socio _Socio = _mapper.Map<Socio>(request);

                Socio _SocioCreado = await _SocioRepositorio.Crear(_Socio);

                if (_SocioCreado.IdSocio != 0)
                    _ResponseDTO = new ResponseDTO<SocioDTO>() { status = true, msg = "ok", value = _mapper.Map<SocioDTO>(_SocioCreado) };
                else
                    _ResponseDTO = new ResponseDTO<SocioDTO>() { status = false, msg = "No se pudo crear el Socio" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<SocioDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] SocioDTO request)
        {
            ResponseDTO<SocioDTO> _ResponseDTO = new ResponseDTO<SocioDTO>();
            try
            {
                Socio _Socio = _mapper.Map<Socio>(request);
                Socio _SocioParaEditar = await _SocioRepositorio.Obtener(u => u.IdSocio == _Socio.IdSocio);

                if (_SocioParaEditar != null)
                {

                    _SocioParaEditar.Nombre = _Socio.Nombre;
                    _SocioParaEditar.Apellido = _Socio.Apellido;
                    _SocioParaEditar.Correo = _Socio.Correo;
                    _SocioParaEditar.Telefono = _Socio.Telefono;
                    _SocioParaEditar.EstadoPago = _Socio.EstadoPago;
                    _SocioParaEditar.FechaInicioMembresia = _Socio.FechaInicioMembresia;
                    _SocioParaEditar.Restricciones = _Socio.Restricciones;
                    _SocioParaEditar.Mutualista = _Socio.Mutualista;
                    _SocioParaEditar.Clave = _Socio.Clave;

                    bool respuesta = await _SocioRepositorio.Editar(_SocioParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<SocioDTO>() { status = true, msg = "ok", value = _mapper.Map<SocioDTO>(_SocioParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<SocioDTO>() { status = false, msg = "No se pudo editar el Socio" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<SocioDTO>() { status = false, msg = "No se encontró el Socio" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<SocioDTO>() { status = false, msg = ex.Message };
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
                Socio _SocioEliminar = await _SocioRepositorio.Obtener(u => u.IdSocio == id);

                if (_SocioEliminar != null)
                {

                    bool respuesta = await _SocioRepositorio.Eliminar(_SocioEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el Socio", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
    }
}
