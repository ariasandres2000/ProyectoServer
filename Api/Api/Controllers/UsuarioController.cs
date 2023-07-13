using Api.DTO;
using Api.Models;
using Api.Services;
using Api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServices _usuarioServices;
        private readonly IConfiguration _config;

        public UsuarioController(DBContext context, IConfiguration config)
        {
            _usuarioServices = new UsuarioServices(context);
            _config = config;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<string> Register(EntUsuario usuario)
        {
            try
            {
                if (!_usuarioServices.ValidarUsuario(usuario.correo))
                    return BadRequest("El usuario se encuentra registrado.");

                _usuarioServices.Registrar(usuario);

                return Ok("Usuario registrado de forma exitosa.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("session")]
        [AllowAnonymous]
        public ActionResult<string> Session(EntLoginDTO login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.correo) && string.IsNullOrEmpty(login.contrasena))
                    return BadRequest("Datos inválidos.");

                EntUsuario usuario = _usuarioServices.Login(login);

                if (usuario == null)
                    return BadRequest("Usuario y/o contraseña incorrectos.");

                return Ok(Herramienta.GenerarToken(_config, usuario.correo.Split('@')[0]));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("UsuarioSesion")]
        public ActionResult<EntUsuario> UsuarioSesion(string correo)
        {
            try
            {
                EntUsuario lUsuario = _usuarioServices.UsuarioSesion(correo);

                if (lUsuario == null)
                    return BadRequest("El usuario no se encuentra registrado.");

                return Ok(lUsuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<EntUsuario>> Get()
        {
            try
            {
                List<EntUsuario> usuario = _usuarioServices.ObtenerUsuario();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<EntUsuario> Get(int id)
        {
            try
            {
                EntUsuario lUsuario = _usuarioServices.ObtenerUsuario(id);

                if (lUsuario == null)
                    return BadRequest("El usuario no se encuentra registrado.");

                return Ok(lUsuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post(EntUsuario usuario) {
            try
            {
                if (!_usuarioServices.ValidarUsuario(usuario.correo))
                    return BadRequest("El usuario se encuentra registrado.");

                _usuarioServices.Registrar(usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        public ActionResult Patch(EntUsuario usuario)
        {
            try
            {
                _usuarioServices.Actualizar(usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _usuarioServices.Eliminar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
