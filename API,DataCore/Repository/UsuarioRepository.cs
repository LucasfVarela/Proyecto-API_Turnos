using API.GenericCore.GenericRepository;
using API_CoreBusiness.DataContext;
using API_CoreBusiness.Entity;
using API_DataCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DataCore.Repository
{
    public class UsuarioRepository : GenericRepository<Usuarios>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext options) : base(options)
        {
            
        }

        public Usuarios? GetByEmail(string Email) => context.Usuario.FirstOrDefault(x => x.Email == Email);
        
        public bool ExisteUsuario(string email ) => context.Usuario.Any(x => x.Email == email);
        
    }
}
