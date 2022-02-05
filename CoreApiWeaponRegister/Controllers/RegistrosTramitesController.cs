
using Microsoft.IdentityModel.Tokens;
using CoreApiWeaponRegister.Entities.Models.RegistrosTramites;
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
    //[Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class RegistrosTramitesController : ControllerBase
    {
        private readonly DataContext ctx;

        private IRegistrosTramitesRepository _perService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RegistrosTramitesController(IRegistrosTramitesRepository perService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _perService = perService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }



        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterTramitesModel model)
        {
            // map model to entity
            var Regtra = _mapper.Map<RegistrosTramites_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Regtra.IDREGISTROTRAMITE.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.Create(Regtra);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite creado con éxito",
                    IdRegistroTramite = Regtra.IDREGISTROTRAMITE,
                    Estado= Regtra.ESTADO

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        [AllowAnonymous]
        [HttpPut("Modify/{idregistrotramite}")]
        public IActionResult Modify(int idregistrotramite,[FromBody] RegisterTramitesModel model)
        {
            // map model to entity
            var Regtra = _mapper.Map<RegistrosTramites_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Regtra.IDREGISTROTRAMITE.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.Modificar(idregistrotramite,Regtra);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite modificado con éxito",
                    IdRegistroTramite = Regtra.IDREGISTROTRAMITE,
                      Estado = Regtra.ESTADO

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramite/{idregistrotramite}")]
        public IActionResult ObtenerRegistrosTramite(int idregistrotramite)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramite(idregistrotramite);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramites_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramitePendiente/{idpersona}/{idtramite}/{idtramiteopcion}")]
        public IActionResult ObtenerRegistrosTramitePendiente(int idpersona, int idtramite, int idtramiteopcion)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramitePendiente(idpersona, idtramite, idtramiteopcion);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramites_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteRuta/{idregistrotramite}")]
        public IActionResult ObtenerRegistrosTramiteRuta(int idregistrotramite)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramiteRuta(idregistrotramite);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesRuta>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpDelete("delete/{idregistrotramite}")]
        public IActionResult delete(int idregistrotramite)
        {
            // map model to entity
            var RegistrosTramiter = _perService.ObtenerRegistrosTramite(idregistrotramite);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, idregistrotramite.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.delete(idregistrotramite);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite modificado con éxito"

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("CambiarEstado")]
        public IActionResult CambiarEstado([FromBody] RegisterTramitesEstadosModel model)
        {
            // map model to entity
         

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.IDREGISTROTRAMITE.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.CambiarEstado(model.IDREGISTROTRAMITE,model.ESTADO,model.MOTIVO,model.USUARIO,model.FECHA);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite creado con éxito",
                    //IdRegistroTramite = Regtra.IDREGISTROTRAMITE,
                    //Estado = Regtra.ESTADO

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteAprobaciones/{idtramite}/{idtramiteopcion}")]
        public IActionResult ObtenerRegistrosTramiteAprobaciones(int idtramite, int idtramiteopcion)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramiteAprobaciones(idtramite, idtramiteopcion);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesAprobacion>>(relacion);
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
