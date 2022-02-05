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
    //public abstract class RegistrosTramitesRepository<T> : IDaperRespository, IGenericRepository<RegistrosTramites_pw>, IRegistrosTramitesRepository where T : class

    public class RegistrosTramitesRepository : GenericRepository<RegistrosTramites_pw>, IRegistrosTramitesRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;

        //public RegistrosTramitesRepository(IConfiguration config, DataContext context) 
        public RegistrosTramitesRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        public RegistrosTramites_pw Create(RegistrosTramites_pw RegistrosTramite)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var RegistrosTramiter = _context.REGREGISTROSTRAMITES_PW.SingleOrDefault(x => x.IDREGISTROTRAMITE == RegistrosTramite.IDREGISTROTRAMITE);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Adicionar(RegistrosTramite);

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

        public IEnumerable<RegistrosTramites_pw> ObtenerRegistrosTramite(int idregistrotramite)
        {
            return FindAll()
                     .Where(ow => ow.IDREGISTROTRAMITE == idregistrotramite)
                     .ToList();
        }

        public IEnumerable<RegistrosTramites_pw> ObtenerRegistrosTramitePendiente(int idpersona, int idtramite, int idtramiteopcion)
        {
            return FindAll()
                    .Where(ow => ow.IDPERSONA == idpersona && ow.IDTRAMITE==idtramite && ow.IDTRAMITEOPCION==idtramiteopcion && (ow.ESTADO=="REGISTRADO" || ow.ESTADO == "RECHAZADO" || ow.ESTADO == "APROBADO"))
                    .ToList();
        }

        public RegistrosTramites_pw Modificar(int idregistrotramite, RegistrosTramites_pw RegistrosTramites)
        {
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var user = _context.REGREGISTROSTRAMITES_PW.SingleOrDefault(x => x.IDREGISTROTRAMITE == idregistrotramite);

            // authentication successful
            return user; //user;

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Update(user);
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

            return RegistrosTramites;

        }

        public IEnumerable<RegistrosTramitesRuta> ObtenerRegistrosTramiteRuta(int idregistrotramite)
        {
            var query = DataContext.REGISTROSTRAMITESRUTA
                   .FromSqlRaw(@"select b.nombre as NombreTramite,c.nombre as NombreTipoTramite,rt.idpersona,rt.idregistrotramite
                            from RELACIONTRAMITESTIPOS_PW a inner join TRAMITES_PW b
                            on a.idtramite=b.idtramite	
                            inner join TIPOSTRAMITES_PW c
                            on a.idtipotramite=c.idtipotramite
                            inner join REGISTROSTRAMITES_PW rt
                            on a.idtramite=rt.idtramite and a.idtipotramite=rt.idtipotramite
                            where rt.idregistrotramite={0}", idregistrotramite);

            return query.ToList();
        }

        public RegistrosTramites_pw delete(int idregistrotramite)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));
            var Registro = _context.REGREGISTROSTRAMITES_PW.SingleOrDefault(x => x.IDREGISTROTRAMITE == idregistrotramite);

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {

                    Delete(Registro);

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

            return Registro;

        }

        public int CambiarEstado(int idregistrotramite, string estadosiguiente, string motivo, string usuario, DateTime fecha)
        {
            var query = DataContext.Database.ExecuteSqlInterpolated($"call REGISTROSTRAMITES_CambiarEstadO ({idregistrotramite} ,{estadosiguiente}, {motivo},{usuario},{fecha})");

            return 1;
        }

        public IEnumerable<RegistrosTramitesAprobacion> ObtenerRegistrosTramiteAprobaciones(int idtramite, int idtramiteopcion)
        {
            var query = DataContext.REGISTROSTRAMITESAPROBACION
                   .FromSqlRaw(@"select rt.idregistrotramite,rt.idpersona, per.nombre as NombrePersona,rt.idtramite, tt.nombre  as NombreTramite, rt.IDTRAMITEOPCION,topc.nombre as NombreTramiteOpcion,
                                   rt.idtipotramite,tpt.nombre as NombreTipoTramite, rt.fechasolicitud,rt.estado
                                 from registrostramites_pw rt inner join personas_pw per
                                 on  rt.idpersona=per.idpersona
                                inner join TRAMITES_PW tt 
                                on tt.idtramite=rt.idtramite
                                inner join TRAMITESOPCIONES_PW topc
                                 on topc.idtramiteopcion=rt.idtramiteopcion
                                inner join TIPOSTRAMITES_PW tpt 
                                on rt.idtipotramite=tpt.idtipotramite
                                where rt.estado='VERIFICACION' and rt.idtramite={0} and rt.IDTRAMITEOPCION={1} ", idtramite, idtramiteopcion);

            return query.ToList();
        }
    }
}
