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
    public class TramitesRepository : GenericRepository<Tramites_pw>, ITramitesRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;
        public TramitesRepository(DataContext context, IConfiguration config, IMapper mapper)
          : base(context)
        {
            _config = config;
            _context = context;
        }


        public IEnumerable<Tramites_pw> ObtenerTramites()
        {

            //return sql.ToList(); 

            var resultado = DataContext.TRAMITES_PW
                       .FromSqlRaw(@"SELECT IDTRAMITE,NOMBRE,FECHAMODIFICACION,USUARIOMOD,ICONO, DESCRIPCION FROM TRAMITES_PW order by IDTRAMITE ");

            return resultado.ToList();

        }


    }
}
