﻿using AutoMapper;
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
namespace CoreApiWeaponRegister.Repository
{
    public class EstadosSiguentesRepository : GenericRepository<EstadosSiguientes_pw>, IEstadosSiguentesRepository
    {

        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;

        public EstadosSiguentesRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        public IEnumerable<EstadosSiguientes_pw> ObtenerEstadosSiguientes()
        {
            return FindAll()
                .OrderBy(ow => ow.ESTADO)
                .ToList();
        }

        public IEnumerable<EstadosSiguientesVista_pw> ObtenerEstadosSiguientesTabla(string Tabla, string Estado, int CambioManual)
        {
      

            var sql = (from es in DataContext.ESTADOSSIGUIENTES_PW
                   join e in DataContext.ESTADOSTABLA_PW on new { TABLA = es.TABLA, ESTADO = es.ESTADOSIGUIENTE } equals new { e.TABLA, e.ESTADO }
                   where es.TABLA == Tabla && es.ESTADO == Estado && es.CAMBIOMANUAL == CambioManual
                   select new EstadosSiguientesVista_pw 
                               { TABLA=es.TABLA,
                                ESTADO=es.ESTADO,
                                ESTADOSIGUIENTE= es.ESTADOSIGUIENTE,
                                NOMBRE= e.NOMBRE, USUARIOMOD= es.USUARIOMOD,
                                FECHAMODIFICACION=es.FECHAMODIFICACION
                   });
            return sql.ToList();

           
        }
    }
}

