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
    //public abstract class RegistrosTramitesArchivosRepository<T> : IDaperRespository, IGenericRepository<RegistrosTramitesArchivos_pw>, IRegistrosTramitesArchivosRepository where T : class

    public class RegistrosTramitesArchivosRepository : GenericRepository<RegistrosTramitesArchivos_pw>, IRegistrosTramitesArchivosRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;


        //public RegistrosTramitesArchivosRepository(IConfiguration config, DataContext context) 
        public RegistrosTramitesArchivosRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        public RegistrosTramitesArchivos_pw Create(RegistrosTramitesArchivos_pw RegistrosTramitesArchivos)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            //var RegistrosTramiter = _context.REGISTROSTRAMITESARCHIVOS_PW.SingleOrDefault(x => x.IDREGISTROTRAMITEDOCUMENTO == RegistrosTramitesArchivos.IDREGISTROTRAMITEDOCUMENTO);


            var blogs = DataContext.OBTNERVALORMAXIMO
                     .FromSqlRaw(@"select NVL2( max(item),  max(item), 0) + 1 as MaximoValor
                                     from REGISTROSTRAMITESARCHIVOS_PW 
                                    where idregistrotramitedocumento={0}", RegistrosTramitesArchivos.IDREGISTROTRAMITEDOCUMENTO);


            RegistrosTramitesArchivos.ITEM = blogs.ToList()[0].MaximoValor;

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Adicionar(RegistrosTramitesArchivos);

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

            return RegistrosTramitesArchivos;

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

        public IEnumerable<RegistrosTramitesArchivos_pw> ObtenerRegistrosTramitesArchivos(int idRegistroTramiteDocumento)
        {
            return FindAll()
                     .Where(ow => ow.IDREGISTROTRAMITEDOCUMENTO == idRegistroTramiteDocumento)
                     .ToList();
        }


        public RegistrosTramitesArchivos_pw Modificar(int idRegistroTramiteDocumento, int item , RegistrosTramitesArchivos_pw RegistrosTramitesArchivos)
        {
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var RegisTraArch = _context.REGISTROSTRAMITESARCHIVOS_PW.SingleOrDefault(x => x.IDREGISTROTRAMITEDOCUMENTO == idRegistroTramiteDocumento && x.ITEM==item);

            // authentication successful
            return RegisTraArch; //user;

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Update(RegisTraArch);
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

            return RegistrosTramitesArchivos;

        }

        public IEnumerable<RegistrosTramitesArchivos_pw> Create_Batch(int idregistrotramite)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));


            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                try
                {

                    var blogs = DataContext.TRAMITEAPLICAFRONTALREVERSO
                     .FromSqlRaw(@"select  a.IDREGISTROTRAMITEDOCUMENTO, a.iddocumento,b.APLICAFRONTALREVERSO
                                from REGISTROSTRAMITESDOCUMENTOS_PW a INNER JOIN DOCUMENTOS_PW b
                                on a.iddocumento=b.iddocumento
                                WHERE a.idregistrotramite={0}
                                and not exists (select * from REGISTROSTRAMITESARCHIVOS_PW c where c.IDREGISTROTRAMITEDOCUMENTO=a.IDREGISTROTRAMITEDOCUMENTO)",  idregistrotramite);



                    //IEnumerable<TramitesDocumentosVista_pw> TraDoc = ITramitesDocumentosRepository.ObtenerTramitesDocumentosConsulta(idtramiteopcion, tipotramite);

                    foreach (var item in blogs.ToList())
                    {

                        var ommax = DataContext.OBTNERVALORMAXIMO
                            .FromSqlRaw(@"select NVL2( max(item),  max(item), 0) + 1 as MaximoValor
                                     from REGISTROSTRAMITESARCHIVOS_PW 
                                    where idregistrotramitedocumento={0}", item.IDREGISTROTRAMITEDOCUMENTO);


                        if (item.APLICAFRONTALREVERSO == 1)
                        {
                            RegistrosTramitesArchivos_pw registro = new RegistrosTramitesArchivos_pw
                            {
                                IDREGISTROTRAMITEDOCUMENTO = item.IDREGISTROTRAMITEDOCUMENTO,
                                ITEM = ommax.ToList()[0].MaximoValor,
                                FECHAMODIFICACION = Utils.ObtenerFechaHoraActual(),
                                USUARIOMOD = "USRSYSMDN",
                                NOMBREARCHIVO = null,
                                FECHACARGA = null,
                                RUTA = null,
                                PARTEDOCUMENTO = "Parte Frontal"
                            };


                            Adicionar(registro);

                            RegistrosTramitesArchivos_pw registro1 = new RegistrosTramitesArchivos_pw
                            {
                                IDREGISTROTRAMITEDOCUMENTO = item.IDREGISTROTRAMITEDOCUMENTO,
                                ITEM = ommax.ToList()[0].MaximoValor+1,
                                FECHAMODIFICACION = Utils.ObtenerFechaHoraActual(),
                                USUARIOMOD = "USRSYSMDN",
                                NOMBREARCHIVO = null,
                                FECHACARGA = null,
                                RUTA = null,
                                PARTEDOCUMENTO = "Parte Reverso"
                            };


                            Adicionar(registro1);
                        }
                        else
                        {
                            RegistrosTramitesArchivos_pw registro3 = new RegistrosTramitesArchivos_pw
                            {
                                IDREGISTROTRAMITEDOCUMENTO = item.IDREGISTROTRAMITEDOCUMENTO,
                                ITEM = ommax.ToList()[0].MaximoValor,
                                FECHAMODIFICACION = Utils.ObtenerFechaHoraActual(),
                                USUARIOMOD = "USRSYSMDN",
                                NOMBREARCHIVO = null,
                                FECHACARGA = null,
                                RUTA = null,
                                PARTEDOCUMENTO = "Parte Frontal"
                            };


                            Adicionar(registro3);
                        }

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
            IEnumerable<RegistrosTramitesArchivos_pw> regTra = ObtenerRegistrosTramitesArchivos(idregistrotramite);
            return regTra;
        }

        public RegistrosTramitesArchivos_pw Modificar_PreCarga(int idRegistroTramiteDocumento, int item, RegistrosTramitesArchivosPrecarga registrosTramitesArchivosPrecarga)
        {
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var RegisTraArch = _context.REGISTROSTRAMITESARCHIVOS_PW.SingleOrDefault(x => x.IDREGISTROTRAMITEDOCUMENTO == idRegistroTramiteDocumento && x.ITEM == item);

            RegisTraArch.FECHACARGA = Utils.ObtenerFechaHoraActual();
            RegisTraArch.NOMBREARCHIVO = registrosTramitesArchivosPrecarga.NOMBREARCHIVO;
            RegisTraArch.RUTA = registrosTramitesArchivosPrecarga.RUTA;
            // authentication successful
            //return RegisTraArch; //user;

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Update(RegisTraArch);
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

            return RegisTraArch;

        }
    }
}
