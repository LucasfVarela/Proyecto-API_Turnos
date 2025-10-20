using API_CoreBusiness.Entity;
using API_CoreBusiness.Request;
using API_CoreBusiness.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.UsesCases.Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioResponse Login(string email, string password);
        UsuarioResponse Registrar(UsuarioRequest usuarioRequest, string password);
        IEnumerable<Usuarios> GetUsuarios();
        string GetToken(UsuarioResponse usuarioResponse);
    }
}
