using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class IndicacionController : ControllerBase
    {
        private readonly IndicacionServices _indicacionServices;

        public IndicacionController(DBContext context)
        {
            _indicacionServices = new IndicacionServices(context);
        }

        [HttpGet("GetIndicacion")]
        public ActionResult<List<EntIndicacion>> GetIndicacion(long idUsuario)
        {
            try
            {
                List<EntIndicacion> indicacion = _indicacionServices.ObtenerIndicacionUsuario(idUsuario);

                return Ok(indicacion);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{idIndicacion}")]
        public ActionResult<EntIndicacion> Get(long idIndicacion)
        {
            try
            {
                EntIndicacion lIndicacion = _indicacionServices.ObtenerIndicacion(idIndicacion);

                if (lIndicacion == null)
                    return NoContent();

                return Ok(lIndicacion);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(EntIndicacion indicacion)
        {
            try
            {
                _indicacionServices.Registrar(indicacion);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        public ActionResult Patch(EntIndicacion indicacion)
        {
            try
            {
                _indicacionServices.Actualizar(indicacion);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{idIndicacion}")]
        public ActionResult Delete(long idIndicacion)
        {
            try
            {
                _indicacionServices.Eliminar(idIndicacion);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
