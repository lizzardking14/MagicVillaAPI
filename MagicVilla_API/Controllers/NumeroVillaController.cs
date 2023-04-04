using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        //se registra el logger
        private readonly ILogger<NumeroVillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        //se inyecta el servicio en el constructor
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numeroRepo, IMapper mapper)
        {
            //se inicializa el servicio
            _logger = logger;
            //_db = db;
            _mapper = mapper;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _response = new();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numero de Villas");

                IEnumerable<NumeroVilla> numerovillaList = await _numeroRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numerovillaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return BadRequest(_response);
        }

        [HttpGet("id:int",Name ="GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener Numero Villa con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }

                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id);


                if (numeroVilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso=false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.statusCode=HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso=false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                //signo de admiracion representa una negacion
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //validacion personalizada con modelstate
                if (await _numeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de Villa con ese Nombre ya existe!");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Obtener(v=>v.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la Villa no existe!");
                    return BadRequest(ModelState);

                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                //await _db.Villas.AddAsync(modelo);
                //await _db.SaveChangesAsync();
                modelo.FechaCreacion=DateTime.Now;
                modelo.FechaActualizacion=DateTime.Now;
                await _numeroRepo.Crear(modelo);
                _response.Resultado= modelo;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNumeroVilla", new { Id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages=new List<string>() { ex.ToString()};
            }

            return _response;

        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso= false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //var villa = VillaStore.villaList.FirstOrDefault(v=>v.Id == id);
                var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id);

                if (numeroVilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //VillaStore.villaList.Remove(villa);
                await _numeroRepo.Remover(numeroVilla);
                //await _db.SaveChangesAsync();
                _response.statusCode=HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.VillaNo)
            {
                _response.IsExitoso=false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            if (await _villaRepo.Obtener(v=>v.Id==updateDto.VillaId)==null)
            {
                ModelState.AddModelError("ClaveForanea","El Id de la Villa no existe!");
                return BadRequest(ModelState);
            }
            NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);

            await _numeroRepo.Actualizar(modelo);
           //await _db.SaveChangesAsync();
           _response.statusCode=HttpStatusCode.NoContent;
            return Ok(_response);
        }

    }
}
