using API.GenericCore.GenericRepository.Interfaces;
using API_CoreBusiness.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.GenericCore.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Insert(T entity) => context.Set<T>().Add(entity); 
        public void Update(T entity) => context.Set<T>().Update(entity);

        public void Delete(int? Id) 
        { 
            if (Id is null || Id == 0) throw new ArgumentNullException(nameof(Id));
            context.Set<T>().Remove(GetById(Id)); 
        }

        public T GetById(int? Id) => context.Set<T>().Find(Id);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)=> context.Set<T>().Where(predicate);

        public IEnumerable<T> GetAll() => context.Set<T>().ToList();
        

    }
}
