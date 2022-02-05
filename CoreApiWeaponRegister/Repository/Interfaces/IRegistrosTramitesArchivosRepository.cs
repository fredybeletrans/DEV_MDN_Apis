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
    public interface IRegistrosTramitesArchivosRepository : IGenericRepository<RegistrosTramitesArchivos_pw>
    {

        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        RegistrosTramitesArchivos_pw Create(RegistrosTramitesArchivos_pw RegistrosTramitesArchivos);
        RegistrosTramitesArchivos_pw Modificar(int idRegistroTramiteDocumento,int item ,RegistrosTramitesArchivos_pw RegistrosTramitesArchivos);
        IEnumerable<RegistrosTramitesArchivos_pw> ObtenerRegistrosTramitesArchivos(int idRegistroTramiteDocumento);
        IEnumerable<RegistrosTramitesArchivos_pw> Create_Batch(int idregistrotramite);
        RegistrosTramitesArchivos_pw Modificar_PreCarga(int idRegistroTramiteDocumento, int item, RegistrosTramitesArchivosPrecarga registrosTramitesArchivosPrecarga);

    }
}
