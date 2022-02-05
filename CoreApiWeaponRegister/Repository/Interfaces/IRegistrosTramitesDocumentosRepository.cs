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
    public interface IRegistrosTramitesDocumentosRepository : IGenericRepository<RegistrosTramitesDocumentos_pw>
    {

        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        RegistrosTramitesDocumentos_pw Create(RegistrosTramitesDocumentos_pw RegistrosTramitesDocumentos);
        RegistrosTramitesDocumentos_pw Modificar(int idregistrotramitedocumento, RegistrosTramitesDocumentos_pw RegistrosTramitesDocumentos);
        IEnumerable<RegistrosTramitesDocumentos_pw> Create_Batch(int idregistrotramite,int idtramiteopcion, int idtipotramite);
        IEnumerable<RegistrosTramitesDocumentos_pw> ObtenerRegistrosTramiteDocumentos(int idregistrotramite);

        IEnumerable<RegistrosTramitesDocumentosVista_pw> ObtenerRegistrosTramiteDocumentosConsulta(int idregistrotramite);
        IEnumerable<RegistrosTramitesDocumentosVista_pw> ObtenerRegistrosTramiteDocumentosById(int idregistrotramitedocumento);

        IEnumerable<RegistrosTramitesDocumentosAprobaciones> ObtenerRegistrosTramiteDocumentosAprobaciones(int idregistrotramitedocumento);

    }
}
