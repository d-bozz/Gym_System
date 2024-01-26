using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using SistemaVentaBlazor.Shared;

namespace SistemaVentaBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioActividadController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHorarioActividadRepositorio _HorarioActividadRepositorio;
        public HorarioActividadController(IHorarioActividadRepositorio HorarioActividadRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _HorarioActividadRepositorio = HorarioActividadRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<HorarioActividadDTO>> _ResponseDTO = new ResponseDTO<List<HorarioActividadDTO>>();

            try
            {
                List<HorarioActividadDTO> ListaHorarioActividads = new List<HorarioActividadDTO>();
                IQueryable<HorarioActividad> query = await _HorarioActividadRepositorio.Consultar();

                ListaHorarioActividads = _mapper.Map<List<HorarioActividadDTO>>(query.ToList());

                if (ListaHorarioActividads.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<HorarioActividadDTO>>() { status = true, msg = "ok", value = ListaHorarioActividads };
                else
                    _ResponseDTO = new ResponseDTO<List<HorarioActividadDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<HorarioActividadDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] HorarioActividadDTO request)
        {
            ResponseDTO<HorarioActividadDTO> _ResponseDTO = new ResponseDTO<HorarioActividadDTO>();
            try
            {
                HorarioActividad _HorarioActividad = _mapper.Map<HorarioActividad>(request);

                HorarioActividad _HorarioActividadCreado = await _HorarioActividadRepositorio.Crear(_HorarioActividad);

                if (_HorarioActividadCreado.IdHorario != 0)
                    _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = true, msg = "ok", value = _mapper.Map<HorarioActividadDTO>(_HorarioActividadCreado) };
                else
                    _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = "No se pudo crear el HorarioActividad" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] HorarioActividadDTO request)
        {
            ResponseDTO<HorarioActividadDTO> _ResponseDTO = new ResponseDTO<HorarioActividadDTO>();
            try
            {
                HorarioActividad _HorarioActividad = _mapper.Map<HorarioActividad>(request);
                HorarioActividad _HorarioActividadParaEditar = await _HorarioActividadRepositorio.Obtener(u => u.IdHorario == _HorarioActividad.IdHorario);

                if (_HorarioActividadParaEditar != null)
                {

                    _HorarioActividadParaEditar.Actividad = _HorarioActividad.Actividad;
                    _HorarioActividadParaEditar.Encargado = _HorarioActividad.Encargado;
                    _HorarioActividadParaEditar.HoraInicio = _HorarioActividad.HoraInicio;
                    _HorarioActividadParaEditar.HoraFin = _HorarioActividad.HoraFin;
                    _HorarioActividadParaEditar.DiaSemana = _HorarioActividad.DiaSemana;

                    bool respuesta = await _HorarioActividadRepositorio.Editar(_HorarioActividadParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = true, msg = "ok", value = _mapper.Map<HorarioActividadDTO>(_HorarioActividadParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = "No se pudo editar el HorarioActividad" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = "No se encontró el HorarioActividad" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = ex.Message };
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
                HorarioActividad _HorarioActividadEliminar = await _HorarioActividadRepositorio.Obtener(u => u.IdHorario == id);

                if (_HorarioActividadEliminar != null)
                {

                    bool respuesta = await _HorarioActividadRepositorio.Eliminar(_HorarioActividadEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el HorarioActividad", value = "" };
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
            ResponseDTO<HorarioActividadDTO> _ResponseDTO = new ResponseDTO<HorarioActividadDTO>();
            try
            {
                HorarioActividad _HorarioActividad = await _HorarioActividadRepositorio.Obtener(u => u.IdHorario == id);

                if (_HorarioActividad != null)
                {
                    HorarioActividadDTO horarioDTO = _mapper.Map<HorarioActividadDTO>(_HorarioActividad);
                    _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = true, msg = "ok", value = horarioDTO };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = "No se encontró el horario", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<HorarioActividadDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

    }
}
