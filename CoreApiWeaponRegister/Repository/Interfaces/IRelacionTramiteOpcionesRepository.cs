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
    public interface IRelacionTramiteOpcionesRepository : IGenericRepository<RelacionTramiteOpciones_pw>
    {

        IEnumerable<RelacionTramiteOpciones_pw> ObtenerRelacionTramiteOpciones(int tipotramite);
        IEnumerable<RelacionTramiteOpcionesVista_pw> ObtenerRelacionTramiteOpc(int tipotramite);
    }
}
