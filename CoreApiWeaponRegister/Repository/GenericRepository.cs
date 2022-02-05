using CoreApiWeaponRegister.Data;
using CoreApiWeaponRegister.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
     
        protected DataContext DataContext { get; set; }

        public GenericRepository(DataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        public void Dispose()
        {

        }

        public IQueryable<T> FindAll()
        {
            return this.DataContext.Set<T>().AsNoTracking();
        }
        
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DataContext.Set<T>().Where(expression).AsNoTracking();
        }
        
        public void Adicionar(T entity)
        {
            this.DataContext.Set<T>().Add(entity);
            this.DataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            
            this.DataContext.Remove(DataContext.Set<T>().FindAsync(id));
            this.DataContext.SaveChanges();

        }

        public void Delete(T entity)
        {

            this.DataContext.Set<T>().Remove(entity);
            this.DataContext.SaveChanges();

        }

        public void Update(T entity)
        {
            this.DataContext.Set<T>().Update(entity);
            this.DataContext.SaveChanges();
        }


        public int Execute(string sp, DynamicParameters parms, CommandType commandType)
        {
            throw new NotImplementedException();
        }

        public T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType)
        {
            throw new NotImplementedException();
           
        }

        public void FindByFields(string _field1, string _field2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            throw new NotImplementedException();
        }
                
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
            throw new NotImplementedException();
        }
    }
}
