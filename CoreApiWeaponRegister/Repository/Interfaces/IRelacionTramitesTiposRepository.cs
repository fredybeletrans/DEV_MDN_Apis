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
    public interface IRelacionTramitesTiposRepository : IGenericRepository<RelacionTramitesTipos_pw>
    {

        IEnumerable<RelacionTramitesTipos_pw> ObtenerRelacionTramitesTipos(int tipotramite);
        IEnumerable<RelacionTramitesTiposVista_pw> ObtenerRelacionTramiteOpc(int tipotramite);
    }
}
