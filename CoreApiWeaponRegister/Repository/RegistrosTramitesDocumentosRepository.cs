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
    //public abstract class RegistrosTramitesDocumentosRepository<T> : IDaperRespository, IGenericRepository<RegistrosTramitesDocumentos_pw>, IRegistrosTramitesDocumentosRepository where T : class

    public class RegistrosTramitesDocumentosRepository : GenericRepository<RegistrosTramitesDocumentos_pw>, IRegistrosTramitesDocumentosRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;


        //public RegistrosTramitesDocumentosRepository(IConfiguration config, DataContext context) 
        public RegistrosTramitesDocumentosRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        public RegistrosTramitesDocumentos_pw Create(RegistrosTramitesDocumentos_pw RegistrosTramitesDocumentos)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var RegistrosTramiter = _context.REGISTROSTRAMITESDOCUMENTOS_PW.SingleOrDefault(x => x.IDREGISTROTRAMITEDOCUMENTO == RegistrosTramitesDocumentos.IDREGISTROTRAMITEDOCUMENTO);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Adicionar(RegistrosTramitesDocumentos);

                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return RegistrosTramiter;

        }

        static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public IEnumerable<RegistrosTramitesDocumentos_pw> ObtenerRegistrosTramiteDocumentos(int idregistrotramite)
        {
            return FindAll()
                     .Where(ow => ow.IDREGISTROTRAMITE == idregistrotramite)
                     .ToList();
        }

        public IEnumerable<RegistrosTramitesDocumentosVista_pw> ObtenerRegistrosTramiteDocumentosConsulta(int idregistrotramite)
        {
            var blogs = DataContext.REGISTROSTRAMITESDOCUMENTOSVISTA_PW
                 .FromSqlRaw(@"select rtd.idregistrotramitedocumento,rtd.idregistrotramite, rtd.iddocumento,doc.nombre as NombreDoc,  
                                rtd.fechamodificacion,rtd.fechavigencia, rtd.estado, rtd.usuariomod,CADUCADO,FECHAEXPIRACION,FECHAEMISION,REQUERIRFECHAEMISION,
                                REQUERIRFECHAEXPIRACION,REQUERIRFECHAVIGENCIA
                                from REGISTROSTRAMITESDOCUMENTOS_PW rtd inner join documentos_pw doc
                                on rtd.iddocumento = doc.iddocumento
                                where rtd.idregistrotramite={0}", idregistrotramite);

            return blogs.ToList();
        }

        public RegistrosTramitesDocumentos_pw Modificar(int idregistrotramitedocumento, RegistrosTramitesDocumentos_pw RegistrosTramitesDocumentos)
        {
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            //var RegisTraDoc = _context.REGISTROSTRAMITESDOCUMENTOS_PW.SingleOrDefault(x => x.IDREGISTROTRAMITE == idregistrotramitedocumento);

            //// authentication successful
            //return RegisTraDoc; //user;

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Update(RegistrosTramitesDocumentos);
                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return RegistrosTramitesDocumentos;

        }

        public IEnumerable<RegistrosTramitesDocumentos_pw> Create_Batch(int idregistrotramite, int idtramiteopcion, int idtipotramite)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));


            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                try
                {
                    
                    var blogs = DataContext.TRAMITESDOCUMENTOSVISTA_PW
                     .FromSqlRaw(@"SELECT TD.idtramiteopcion, tOPC.Nombre as NombreOpcion, TD.idtipotramite, tpt.nombre as NombreTipoTramite, TD.iddocumento,doc.Nombre as NombreDoc, TD.ESTADO
                                FROM TRAMITESDOCUMENTOS_PW TD INNER JOIN  tramitesopciones_pw Topc
                                ON TD.idtramiteopcion=Topc.idtramiteopcion
                                inner join tipostramites_pw tpt 
                                on  tpt.idtipotramite=TD.idtipotramite
                                inner join documentos_pw doc
                                on TD.iddocumento=doc.iddocumento
                                WHERE TD.idtramiteopcion={0} and TD.idtipotramite={1} AND TD.ESTADO='A'
                                  and not exists (SELECT * FROM REGISTROSTRAMITESDOCUMENTOS_PW rt where idregistrotramite={2} and rt.iddocumento=TD.iddocumento)", idtramiteopcion, idtipotramite, idregistrotramite);

                   

                    //IEnumerable<TramitesDocumentosVista_pw> TraDoc = ITramitesDocumentosRepository.ObtenerTramitesDocumentosConsulta(idtramiteopcion, tipotramite);

                    foreach (var item in blogs.ToList())
                    {

                        RegistrosTramitesDocumentos_pw registro = new RegistrosTramitesDocumentos_pw
                        {
                            IDREGISTROTRAMITEDOCUMENTO = 0,
                            IDDOCUMENTO = item.IDDOCUMENTO,
                            FECHAMODIFICACION = Utils.ObtenerFechaHoraActual(),
                            USUARIOMOD = "USRSYSMDN",
                            ESTADO = "ENTREGADO",
                            FECHAVIGENCIA = null,
                            IDREGISTROTRAMITE = idregistrotramite
                        };


                        Adicionar(registro);
                    }



                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            IEnumerable<RegistrosTramitesDocumentos_pw> regTra = ObtenerRegistrosTramiteDocumentos(idregistrotramite);
            return regTra;
        }

        private DateTime? ObtenerFechaHoraActual()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistrosTramitesDocumentosVista_pw> ObtenerRegistrosTramiteDocumentosById(int idregistrotramitedocumento)
        {
            var blogs = DataContext.REGISTROSTRAMITESDOCUMENTOSVISTA_PW
                 .FromSqlRaw(@"select rtd.idregistrotramitedocumento,rtd.idregistrotramite, rtd.iddocumento,doc.nombre as NombreDoc,  
                                rtd.fechamodificacion,rtd.fechavigencia, rtd.estado, rtd.usuariomod,CADUCADO,FECHAEXPIRACION,FECHAEMISION,REQUERIRFECHAEMISION,
                                REQUERIRFECHAEXPIRACION,REQUERIRFECHAVIGENCIA
                                from REGISTROSTRAMITESDOCUMENTOS_PW rtd inner join documentos_pw doc
                                on rtd.iddocumento = doc.iddocumento
                                where rtd.idregistrotramitedocumento={0}", idregistrotramitedocumento);

            return blogs.ToList();
        }

        public IEnumerable<RegistrosTramitesDocumentosAprobaciones> ObtenerRegistrosTramiteDocumentosAprobaciones(int idregistrotramitedocumento)
        {
            var blogs = DataContext.REGISTROSTRAMITESDOCUMENTOSAPROBACIONES
              .FromSqlRaw(@"select rtd.idregistrotramitedocumento, rta.item, rtd.iddocumento,dpw.nombre as NombreDocumento,rtd.idregistrotramite,rta.partedocumento, rta.nombrearchivo,rta.ruta,
                             rt.idpersona, per.nombre as NombrePersona,rt.idtramite, tt.nombre  as NombreTramite, rt.IDTRAMITEOPCION,topc.nombre as NombreTramiteOpcion,
                               rt.idtipotramite,tpt.nombre as NombreTipoTramite
                             from REGISTROSTRAMITESDOCUMENTOS_PW rtd inner join REGISTROSTRAMITESARCHIVOS_PW rta
                            on rtd.idregistrotramitedocumento=rta.idregistrotramitedocumento
                            inner join DOCUMENTOS_PW dpw 
                            on rtd.iddocumento=dpw.iddocumento
                            inner join registrostramites_pw rt  
                             on rt.idregistrotramite=rtd.idregistrotramite
                            inner join personas_pw per
                             on  rt.idpersona=per.idpersona
                            inner join TRAMITES_PW tt 
                            on tt.idtramite=rt.idtramite
                            inner join TRAMITESOPCIONES_PW topc
                             on topc.idtramiteopcion=rt.idtramiteopcion
                            inner join TIPOSTRAMITES_PW tpt 
                            on rt.idtipotramite=tpt.idtipotramite
                            where  rtd.idregistrotramite={0} order by dpw.nombre", idregistrotramitedocumento);

            return blogs.ToList();
        }
    }
}
