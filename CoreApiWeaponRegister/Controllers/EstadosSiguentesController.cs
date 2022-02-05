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

namespace CoreApiWeaponRegister.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class EstadosSiguentesController : ControllerBase
    {
        private readonly DataContext ctx;

        private IEstadosSiguentesRepository _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public EstadosSiguentesController(IEstadosSiguentesRepository userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("ObtenerEstadosSiguientes")]
        public IActionResult ObtenerEstadosxTabla()
        {
            try
            {
                var estados = _userService.ObtenerEstadosSiguientes();
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<EstadosSiguientes_pw>>(estados);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("ObtenerEstadosSiguientesTabla/{Tabla}/{Estado}/{CambioManual}")]
        public IActionResult ObtenerEstadosSiguientesTabla(string Tabla, string Estado, int CambioManual)
        {
            try
            {
                var estados = _userService.ObtenerEstadosSiguientesTabla(Tabla, Estado, CambioManual);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<EstadosSiguientesVista_pw>>(estados);
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
