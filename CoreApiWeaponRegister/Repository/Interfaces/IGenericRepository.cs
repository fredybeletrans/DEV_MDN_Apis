using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Repository.Interfaces
{
    public interface IGenericRepository<T> : IDisposable    // where T : class
    {
        //DbConnection GetDbconnection();

        IEnumerable<T> Get();
        
        T Get(int id);
        
        void Adicionar(T entity);

        //T Adicion<T>(T entity);

        //Task<bool> Adicion(T entity);

        //void Adicion(T entity);
        
        void Delete(int id);

        void Delete(T entity);

        void Update(T entity);

        void FindByFields(string _field1, string _field2);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        IQueryable<T> FindAll();

        //T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //List<T> GetAll<T>(string query, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
    }
}
