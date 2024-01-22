using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using SistemaVentaBlazor.Server.Repositorio.Implementacion;
using SistemaVentaBlazor.Shared;

namespace SistemaVentaBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepositorio = categoriaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<CategoriaDTO>> _response = new ResponseDTO<List<CategoriaDTO>>();

            try
            {
                List<CategoriaDTO> _listaCategorias = new List<CategoriaDTO>();
                _listaCategorias = _mapper.Map<List<CategoriaDTO>>(await _categoriaRepositorio.Lista());

                if (_listaCategorias.Count > 0)
                    _response = new ResponseDTO<List<CategoriaDTO>>() { status = true, msg = "ok", value = _listaCategorias };
                else
                    _response = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = "sin resultados", value = null };


                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CategoriaDTO request)
        {
            ResponseDTO<CategoriaDTO> _ResponseDTO = new ResponseDTO<CategoriaDTO>();
            try
            {
                Categoria _Categoria = _mapper.Map<Categoria>(request);

                Categoria _CategoriaCreado = await _categoriaRepositorio.Crear(_Categoria);

                if (_CategoriaCreado.IdCategoria != 0)
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(_CategoriaCreado) };
                else
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se pudo crear la Categoria" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO request)
        {
            ResponseDTO<CategoriaDTO> _ResponseDTO = new ResponseDTO<CategoriaDTO>();
            try
            {
                Categoria _categoria = _mapper.Map<Categoria>(request);
                Categoria _CategoriaParaEditar = await _categoriaRepositorio.Obtener(u => u.IdCategoria == _categoria.IdCategoria);

                if (_CategoriaParaEditar != null)
                {

                    _CategoriaParaEditar.Descripcion = _categoria.Descripcion;

                    bool respuesta = await _categoriaRepositorio.Editar(_CategoriaParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(_CategoriaParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se pudo editar la Categoria" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se encontró la Categoria" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = ex.Message };
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
                Categoria _CategoriaEliminar = await _categoriaRepositorio.Obtener(u => u.IdCategoria == id);
                if (_CategoriaEliminar != null)
                {

                    bool respuesta = await _categoriaRepositorio.Eliminar(_CategoriaEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar la cetegoria", value = "" };
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
            ResponseDTO<CategoriaDTO> _ResponseDTO = new ResponseDTO<CategoriaDTO>();
            try
            {
                Categoria _Categoria = await _categoriaRepositorio.Obtener(u => u.IdCategoria == id);

                if (_Categoria != null)
                {
                    CategoriaDTO categoriaDTO = _mapper.Map<CategoriaDTO>(_Categoria);
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = true, msg = "ok", value = categoriaDTO };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = "No se encontró la categoria", value = null };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<CategoriaDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }
    }
}
