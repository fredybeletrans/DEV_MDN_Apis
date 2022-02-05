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
    public class RelacionTramitesTiposRepository : GenericRepository<RelacionTramitesTipos_pw>, IRelacionTramitesTiposRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;
        public RelacionTramitesTiposRepository(DataContext context, IConfiguration config, IMapper mapper)
          : base(context)
        {
            _config = config;
            _context = context;
        }
        public IEnumerable<RelacionTramitesTipos_pw> ObtenerRelacionTramitesTipos(int idtramite)
        {
            return FindAll()
               .Where(ow => ow.IDTRAMITE == idtramite)
               .ToList();

        }

        public IEnumerable<RelacionTramitesTiposVista_pw> ObtenerRelacionTramiteOpc(int idtramite)
        {

            var blogs = DataContext.RELACIONTRAMITESTIPOSVISTA_PW
                .FromSqlRaw(@"select a.IDTRAMITE, a.IDTIPOTRAMITE, b.NOMBRE,  a.FECHAMODIFICACION,a.USUARIOMOD
                            from RelacionTramitesTipos_PW a inner join TIPOSTRAMITES_PW b
                            on a.IDTIPOTRAMITE=b.IDTIPOTRAMITE
                            where a.idtramite={0}", idtramite);

            return blogs.ToList();

        }


    }
}
