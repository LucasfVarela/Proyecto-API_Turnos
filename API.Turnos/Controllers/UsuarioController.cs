using API.UsesCases.Services.Interfaces;
using API.UsesCases.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Turnos.Controllers
{
    [Route("API/")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsuarioService usuarioService;


        public UsuarioController(IUnitOfWork unitOfWork, IUsuarioService usuarioService)
        {
            this.unitOfWork = unitOfWork;
            this.usuarioService = usuarioService;
        }

        [HttpGet("GetUsuarios")]
        public ActionResult GetUsuarios()
        {
            var response = usuarioService.GetUsuarios();
            if (response is null) { return BadRequest("NOT FOUND"); }
            return Ok(response);
        }
    }
}
