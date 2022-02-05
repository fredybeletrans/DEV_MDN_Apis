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
    public interface IPersonasRepository : IGenericRepository<Personas_pw>
    {

        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        Personas_pw Create(Personas_pw personas);
        IEnumerable<Personas_pw> ObtenerPersona(string user);

    }
}
