using API.UsesCases.Services;
using API.UsesCases.Services.Interfaces;
using API.UsesCases.UnitOfWork.Interfaces;
using API_CoreBusiness.Request;
using API_CoreBusiness.Response;
using API_DataCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Turnos.Controllers
{
    [Route("API/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsuarioService usuarioService;


        public LoginController(IUnitOfWork unitOfWork, IUsuarioService usuarioService)
        {
            this.unitOfWork = unitOfWork;
            this.usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] UsuarioRequest request)
        {
            var response = usuarioService.Login(request.Email, request.Password);
            if (response is null) { return BadRequest("Contraseña incorrecta"); }
            var token = usuarioService.GetToken(response);
            return Ok(new
            {
                token = token,
                usuario = response,
            });
        }

        [HttpPost("Registrar")]
        public ActionResult Registrar([FromBody] UsuarioRequest request)
        {
            if (unitOfWork.UsuarioRepository.ExisteUsuario(request.Email.ToLower())) return BadRequest("");
            UsuarioResponse response = usuarioService.Registrar(request,request.Password);
            return Ok(response);
        }
    }
}

