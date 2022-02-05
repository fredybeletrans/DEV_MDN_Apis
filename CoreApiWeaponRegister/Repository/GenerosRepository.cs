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
    public class GenerosRepository : GenericRepository<Generos_pw>, IGeneroRepository
    {
        static private IConfiguration _config;
        private DataContext _context;
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;

        public GenerosRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        public IEnumerable<Generos_pw> ObtenerGeneros()
        {
            return FindAll()
                .OrderBy(ow => ow.IDGENERO)
                .ToList();
        }
    }
}