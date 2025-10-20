using API_CoreBusiness.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_CoreBusiness.Response
{
    public class UsuarioResponse
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string EMail { get; set; }
        public Role Role { get; set; }
        public DateTime Fecha_Add { get; set; }
        public DateTime Fecha_Mod { get; set; }
    }
}
