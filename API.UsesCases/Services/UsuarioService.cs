using API.UsesCases.Services.Interfaces;
using API.UsesCases.UnitOfWork.Interfaces;
using API_CoreBusiness.DataContext;
using API_CoreBusiness.Entity;
using API_CoreBusiness.Request;
using API_CoreBusiness.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.UsesCases.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IConfiguration Configuration;


        public UsuarioService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.UnitOfWork = unitOfWork;
            this.Configuration = configuration;
        }


        public UsuarioResponse Login(string email, string password)
        {

            Usuarios? Usuario = UnitOfWork.UsuarioRepository.GetByEmail(email);

            if (Usuario is not null)
            {
                UsuarioResponse response = new UsuarioResponse();
                if (!ValidPassword(password, Usuario.PasswordSalt, Usuario.PasswordHash))
                {
                    return null;
                }
                response.Id = Usuario.Id;
                response.Role = Usuario.Role;
                response.EMail = email;
                response.Nombre = Usuario.Nombre;
                response.Fecha_Add = Usuario.Fecha_Add;
                response.Fecha_Mod = Usuario.Fecha_Mod;
                return response;
            }
            return null;
        }

        public UsuarioResponse Registrar(UsuarioRequest usuarioRequest, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            BuildPassword(password, out passwordHash, out passwordSalt);
            Usuarios NewUsuario = new Usuarios();
            NewUsuario.Nombre = usuarioRequest.Nombre;
            NewUsuario.Email = usuarioRequest.Email;
            NewUsuario.PasswordHash = passwordHash;
            NewUsuario.PasswordSalt = passwordSalt;

            //Esto hay que definir que venga el roll desde el front
            NewUsuario.Role = Role.Cliente;

            UnitOfWork.UsuarioRepository.Insert(NewUsuario);
            UnitOfWork.Save();

            return new UsuarioResponse()
            {
                Id = NewUsuario.Id,
                Role = NewUsuario.Role,
                EMail = NewUsuario.Email,
                Nombre = NewUsuario.Nombre,
                Fecha_Add = DateTime.Now,
            };

        }

        public string GetToken(UsuarioResponse usuarioResponse)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioResponse.EMail),
                new Claim(JwtRegisteredClaimNames.NameId,usuarioResponse.Id.ToString()),
                new Claim(ClaimTypes.Role, usuarioResponse.Role.ToString()),
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(120),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Issuer = Configuration["Jwt:Issuing"],
                Audience = Configuration["Jwt:Audience"]
            };

            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<Usuarios> GetUsuarios()
        {
            return UnitOfWork.UsuarioRepository.GetAll();
        }

        #region Private method
        private void BuildPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            HMACSHA512 hMac = new HMACSHA512();
            passwordSalt = hMac.Key;
            passwordHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private bool ValidPassword(string password, byte[] passSalt, byte[] passHash)
        {

            HMACSHA512 hMac = new HMACSHA512(passSalt);
            byte[] hash = hMac.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Implemente branchless tengo que ser si me funciona , sino dejo el if normal

            int diff = 0;
            for (int i = 0; i < hash.Length; i++)
            {
                diff |= hash[i] ^ passHash[i];
            }
            return diff == 0;


            //for (int i = 0; i < hash.Length; i++)
            //{
            //    if (hash[i] != passHash[i]) return false;
            //}
            //return true
        }
        #endregion
    }

}
