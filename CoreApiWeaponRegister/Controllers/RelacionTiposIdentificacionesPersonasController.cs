using CoreApiWeaponRegister.Helpers;
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
    public class RelacionTiposIdentificacionesPersonasController : ControllerBase
    {
        private readonly DataContext ctx;

        private IRelacionTiposIdentificacionesPersonasRepository _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public RelacionTiposIdentificacionesPersonasController(IRelacionTiposIdentificacionesPersonasRepository userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        //[HttpGet("{tipopersona}", Name = "ObtenerRelacionIdentificacionPersona")]
        [HttpGet("ObtenerRelacionIdentificacionPersona/{tipopersona}")]
        public IActionResult GetAll(int tipopersona)
        {
            try
            {
                var relacion = _userService.ObtenerRelacionIdentificacionPersona(tipopersona);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RelacionTiposIdentificacionesPersonas_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{tipopersona}", Name = "ObtenerIdentificacion")]
        [HttpGet("ObtenerIdentificacion/{tipopersona}")]
        public IActionResult GetObtenerIdentificacion(int tipopersona)
        {
            try
            {
                var relacion = _userService.ObtenerIdentificacion(tipopersona);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RelacionTiposIdentificacionesPersonasVista_pw>>(relacion);
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
