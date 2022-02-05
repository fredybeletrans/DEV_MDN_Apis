using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
using CoreApiWeaponRegister.Entities.Models.Usuarios;
using CoreApiWeaponRegister.Repository;
using CoreApiWeaponRegister.Repository.Interfaces;

namespace CoreApiWeaponRegister.Repository.Interfaces
{
    public interface IEstadoCivilRepository : IGenericRepository<EstadoCivil_pw>
    {
        //IEnumerable<EstadoCivil_pw> BucarTodos();
        //IQueryable<EstadoCivil_pw> ObtenerEstadoscivil();

        IEnumerable<EstadoCivil_pw> ObtenerEstadosCiviles();
    }
}
