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
    //public abstract class PersonasRepository<T> : IDaperRespository, IGenericRepository<Personas_pw>, IPersonasRepository where T : class

    public class PersonasRepository : GenericRepository<Personas_pw>, IPersonasRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;

        //public PersonasRepository(IConfiguration config, DataContext context) 
        public PersonasRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        //public T Adicion<T>(Personas_pw entity)
        //T Adicion<T>(Personas_pw entity)
        //public async Task<bool> Adicion(Personas_pw entity)

        //public Personas_pw Authenticate(Personas_pw model, string personaname, string password)
        //public AuthenticateResponse Authenticate(AuthenticateRequest model)


        public Personas_pw Create(Personas_pw persona)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var personar = _context.PERSONAS_PW.SingleOrDefault(x => x.IDPERSONA == persona.IDPERSONA);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {
                    
                    Adicionar(persona);

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

            return personar;

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

        public IEnumerable<Personas_pw> ObtenerPersona(string user)
        {

            return FindAll()
                     .Where(ow => ow.USUARIO == user)
                     .ToList();

            //var blogs = DataContext.PERSONAS_PW
            //    .FromSqlRaw(@"select IDPERSONA  ,
            //                IDTIPOPERSONA ,
            //                IDTIPOIDENTIFICACIONPERSONAL,
            //                NUMDOCUMENTO ,
            //                NIT ,
            //                PRIMERNOMBRE,
            //                SEGUNDONOMBRE  ,
            //                TERCERNOMBRE  ,
            //                PRIMERAPELLIDO,
            //                SEGUNDOAPELLIDO  ,
            //                IDGENERO  ,
            //                RAZONSOCIAL,
            //                NOMBRE ,
            //                NODOCUMENTORESIDENTE,
            //                ORGESTATALMINISTERIO ,
            //                FECHANACIMIENTO ,
            //                FECHACREACION ,
            //                USUARIO ,
            //                FECHAMODIFICACION,
            //                USUARIOMOD ,
            //                PARTICULA ,
            //                IDESTADOCIVIL 
            //                from personas_pw");

            //return blogs.ToList();

        }




    }
}
