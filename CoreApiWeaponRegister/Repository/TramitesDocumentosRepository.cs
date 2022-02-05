using AutoMapper;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWeaponRegister.Repository.Interfaces;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
using CoreApiWeaponRegister.Data;
using CoreApiWeaponRegister.Helpers;
using CoreApiWeaponRegister.Entities.Models.Usuarios;
using System.Data.Common;
using static System.Data.CommandType;
using System.Data.OracleClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BC = BCrypt.Net.BCrypt;
using System.Data.Entity;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CoreApiWeaponRegister.Repository
{
    public class TramitesDocumentosRepository : GenericRepository<TramitesDocumentos_pw>, ITramitesDocumentosRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;
        public TramitesDocumentosRepository(DataContext context, IConfiguration config, IMapper mapper)
          : base(context)
        {
            _config = config;
            _context = context;
        }
        public IEnumerable<TramitesDocumentos_pw> ObtenerTramitesDocumentos(int idtramiteopcion, int idtipotramite)
        {
            return FindAll()
               .Where(ow => ow.IDTRAMITEOPCION == idtramiteopcion && ow.IDTIPOTRAMITE==idtipotramite)
               .ToList();

        }

        public IEnumerable<TramitesDocumentosVista_pw> ObtenerTramitesDocumentosConsulta(int idtramiteopcion, int idtipotramite)
        {

            var blogs = DataContext.TRAMITESDOCUMENTOSVISTA_PW
                .FromSqlRaw(@"SELECT TD.idtramiteopcion, tOPC.Nombre as NombreOpcion, TD.idtipotramite, tpt.nombre as NombreTipoTramite, TD.iddocumento,doc.Nombre as NombreDoc, TD.ESTADO
                                FROM TRAMITESDOCUMENTOS_PW TD INNER JOIN  tramitesopciones_pw Topc
                                ON TD.idtramiteopcion=Topc.idtramiteopcion
                                inner join tipostramites_pw tpt 
                                on  tpt.idtipotramite=TD.idtipotramite
                                inner join documentos_pw doc
                                on TD.iddocumento=doc.iddocumento
                                WHERE TD.idtramiteopcion={0} and TD.idtipotramite={1} AND TD.ESTADO='A'", idtramiteopcion,idtipotramite);

            return blogs.ToList();

        }


    }
}
