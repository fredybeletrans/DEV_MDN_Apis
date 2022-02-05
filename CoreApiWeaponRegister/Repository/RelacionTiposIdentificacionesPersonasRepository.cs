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
    public class RelacionTiposIdentificacionesPersonasRepository : GenericRepository<RelacionTiposIdentificacionesPersonas_pw>,IRelacionTiposIdentificacionesPersonasRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;
        public RelacionTiposIdentificacionesPersonasRepository(DataContext context, IConfiguration config, IMapper mapper)
          : base(context)
        {
            _config = config;
            _context = context;
        }
        public IEnumerable<RelacionTiposIdentificacionesPersonas_pw> ObtenerRelacionIdentificacionPersona(int tipopersona)
        {
            return FindAll()
               .Where(ow => ow.IDTIPOPERSONA == tipopersona)
               .ToList();

        }

        public IEnumerable<RelacionTiposIdentificacionesPersonasVista_pw> ObtenerIdentificacion(int tipopersona)
        {

            //var sql = (
            //            from a in DataContext.TIPOSIDENTIFICACIONESPERSONALES_PW
            //            join b in DataContext.RELACIONTIPOSIDENTIFICACIONESPERSONAS_PW on a.IDTIPOIDENTIFICACIONPERSONAL equals b.IDTIPOIDENTIFICACIONPERSONAL
            //            where b.IDTIPOPERSONA == tipopersona
            //            select new RelacionTiposIdentificacionesPersonasVista_pw
            //            {
            //                IDTIPOPERSONA = b.IDTIPOPERSONA,
            //                IDTIPOIDENTIFICACIONPERSONAL = a.IDTIPOIDENTIFICACIONPERSONAL,
            //                NOMBRE = a.NOMBRE,
            //                MASCARA = a.MASCARA
            //            });
            //return sql.ToList(); 

            var blogs = DataContext.RELACIONTIPOSIDENTIFICACIONESPERSONASVISTA_PW
                .FromSqlRaw(@"select b.IDTIPOPERSONA,
                               a.IDTIPOIDENTIFICACIONPERSONAL,
                               a.NOMBRE,
                             a.mascara
                        from TIPOSIDENTIFICACIONESPERSONALES_PW a inner
                        join RELACIONTIPOSIDENTIFICACIONESPERSONAS_PW b
                        on a.IDTIPOIDENTIFICACIONPERSONAL = b.IDTIPOIDENTIFICACIONPERSONAL
                        where b.IDTIPOPERSONA = {0}", tipopersona);

            return blogs.ToList();

        }

        
    }
}
