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
    public interface IRelacionTiposIdentificacionesPersonasRepository:IGenericRepository<RelacionTiposIdentificacionesPersonas_pw>
    {

        IEnumerable<RelacionTiposIdentificacionesPersonas_pw> ObtenerRelacionIdentificacionPersona(int tipopersona);
        IEnumerable<RelacionTiposIdentificacionesPersonasVista_pw> ObtenerIdentificacion(int tipopersona);
    }
}
