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
    public class RelacionTramiteOpcionesRepository : GenericRepository<RelacionTramiteOpciones_pw>, IRelacionTramiteOpcionesRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;
        public RelacionTramiteOpcionesRepository(DataContext context, IConfiguration config, IMapper mapper)
          : base(context)
        {
            _config = config;
            _context = context;
        }
        public IEnumerable<RelacionTramiteOpciones_pw> ObtenerRelacionTramiteOpciones(int idtramite)
        {
            return FindAll()
               .Where(ow => ow.IDTRAMITE == idtramite)
               .ToList();

        }

        public IEnumerable<RelacionTramiteOpcionesVista_pw> ObtenerRelacionTramiteOpc(int idtramite)
        {

            var blogs = DataContext.RELACIONTRAMITEOPCIONESVISTA_PW
                .FromSqlRaw(@"select a.IDTRAMITE, a.IDTRAMITEOPCION, b.NOMBRE,  a.FECHAMODIFICACION,a.USUARIOMOD,b.ICONO
                            from RELACIONTRAMITEOPCIONES_PW a inner join TRAMITESOPCIONES_PW b
                            on a.idtramiteopcion=b.idtramiteopcion
                            where a.idtramite={0} order by a.IDTRAMITEOPCION", idtramite);

            return blogs.ToList();

        }


    }
}
