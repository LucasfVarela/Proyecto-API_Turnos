using API.UsesCases.UnitOfWork.Interfaces;
using API_CoreBusiness.DataContext;
using API_DataCore.Interfaces;
using API_DataCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.UsesCases.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IUsuarioRepository UsuarioRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            UsuarioRepository = new UsuarioRepository(context);
        }


        public void Dispose()
        {
            context.Dispose();

        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
