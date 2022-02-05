﻿using CoreApiWeaponRegister.Helpers;
using CoreApiWeaponRegister.Data;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
using CoreApiWeaponRegister.Entities.Models.Usuarios;
using CoreApiWeaponRegister.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
//using Microsoft.IdentityModel.Tokens;

namespace CoreApiWeaponRegister.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TramitesDocumentosController : ControllerBase
    {
        private readonly DataContext ctx;

        private ITramitesDocumentosRepository _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public TramitesDocumentosController(ITramitesDocumentosRepository userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("ObtenerTramitesDocumentos/{idtramiteopcion}/{idtipotramite}")]
        public IActionResult GetAll(int idtramiteopcion, int idtipotramite)
        {
            try
            {
                var relacion = _userService.ObtenerTramitesDocumentos(idtramiteopcion, idtipotramite);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<TramitesDocumentos_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{idtramite}", Name = "ObtenerIdentificacion")]
        [HttpGet("ObtenerTramitesDocumentosConsulta/{idtramiteopcion}/{idtipotramite}")]
        public IActionResult ObtenerTramitesDocumentosConsulta(int idtramiteopcion, int idtipotramite)
        {
            try
            {
                var relacion = _userService.ObtenerTramitesDocumentosConsulta(idtramiteopcion, idtipotramite);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<TramitesDocumentosVista_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
