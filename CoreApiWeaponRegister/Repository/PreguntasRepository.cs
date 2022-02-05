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
    public class PreguntasRepository : GenericRepository<Preguntas_pw>, IPreguntasRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;
        public PreguntasRepository(DataContext context, IConfiguration config, IMapper mapper)
          : base(context)
        {
            _config = config;
            _context = context;
        }
       

        public IEnumerable<Preguntas_pw> ObtenerPreguntas()
        {

            //return sql.ToList(); 

            var blogs = DataContext.PREGUNTAS_PW
                       .FromSqlRaw(@"SELECT pp.IDPREGUNTA, pp.DESCRIPCION, pp.IDGRUPOPREGUNTA, pp.IDTIPORESPUESTA, TIPODATO
                FROM PORTALMDF.PREGUNTAS_PW pp inner join TIPOSRESPUESTAS_PW tr
                on pp.idtiporespuesta=tr.idtiporespuesta");

            return blogs.ToList();

        }


    }
}
