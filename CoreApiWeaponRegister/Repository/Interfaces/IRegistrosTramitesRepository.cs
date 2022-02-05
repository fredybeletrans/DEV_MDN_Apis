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
    public interface IRegistrosTramitesRepository : IGenericRepository<RegistrosTramites_pw>
    {

        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        RegistrosTramites_pw Create(RegistrosTramites_pw RegistrosTramites);
        RegistrosTramites_pw Modificar(int idregistrotramite,RegistrosTramites_pw RegistrosTramites);
        RegistrosTramites_pw delete ( int idregistrotramite);
        int CambiarEstado(int idregistrotramite , string estadosiguiente , string motivo, string usuario, DateTime fecha );
        IEnumerable<RegistrosTramites_pw> ObtenerRegistrosTramite(int idregistrotramite);
        IEnumerable<RegistrosTramites_pw> ObtenerRegistrosTramitePendiente(int idpersona,int idtramite, int idtramiteopcion);
        IEnumerable<RegistrosTramitesRuta> ObtenerRegistrosTramiteRuta(int idregistrotramite);
        IEnumerable<RegistrosTramitesAprobacion> ObtenerRegistrosTramiteAprobaciones( int idtramite , int idtramiteopcion);

    }
}
