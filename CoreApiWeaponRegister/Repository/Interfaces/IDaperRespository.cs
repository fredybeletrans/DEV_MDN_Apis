using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Repository.Interfaces
{
    public interface IDaperRespository : IDisposable
    {
        DbConnection GetDbconnection();
        //T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //List<T> GetAll<T>(string query, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
    }
}
