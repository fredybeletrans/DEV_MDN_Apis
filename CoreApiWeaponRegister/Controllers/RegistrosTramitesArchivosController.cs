
using Microsoft.IdentityModel.Tokens;
using CoreApiWeaponRegister.Entities.Models.RegistrosTramitesArchivos;
using CoreApiWeaponRegister.Helpers;
using CoreApiWeaponRegister.Data;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
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
    public class RegistrosTramitesArchivosController : ControllerBase
    {
        private readonly DataContext ctx;

        private IRegistrosTramitesArchivosRepository _perService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RegistrosTramitesArchivosController(IRegistrosTramitesArchivosRepository perService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _perService = perService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }



        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterTramitesArchModel model)
        {
            // map model to entity
            var Regtra = _mapper.Map<RegistrosTramitesArchivos_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Regtra.IDREGISTROTRAMITEDOCUMENTO.ToString())
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
                    IdRegistroTramite = Regtra.IDREGISTROTRAMITEDOCUMENTO

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPut("Modify/{idRegistroTramiteDocumento}/{item}")]
        public IActionResult Modify(int idRegistroTramiteDocumento, int item, [FromBody] RegisterTramitesArchModel model)
        {
            // map model to entity
            var Regtra = _mapper.Map<RegistrosTramitesArchivos_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Regtra.IDREGISTROTRAMITEDOCUMENTO.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.Modificar(idRegistroTramiteDocumento,item, Regtra);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite modificado con éxito",
                    IdRegistroTramite = Regtra.IDREGISTROTRAMITEDOCUMENTO

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

       
        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteArchivos/{idRegistroTramiteDocumento}")]
        public IActionResult ObtenerRegistrosTramiteArchivos(int idRegistroTramiteDocumento)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramitesArchivos(idRegistroTramiteDocumento);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesArchivos_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpPost("register_batch")]
        public IActionResult register_batch([FromBody] Dictionary<string,object> model)
        {
            //var Regtra = _mapper.Map<RegistrosTramitesDocumentos_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model["idregistrotramite"].ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            try
            {
                // create per
                _perService.Create_Batch( int.Parse(model["idregistrotramite"].ToString()));
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite creado con éxito",
                    IdRegistroTramite = int.Parse(model["idregistrotramite"].ToString())

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPut("Modify_PreCarga/{idRegistroTramiteDocumento}/{item}")]
        public IActionResult Modify_PreCarga(int idRegistroTramiteDocumento, int item, [FromBody] ModifiPreCargaModel model)
        {
            //map model to entity
            var Regtra = _mapper.Map<RegistrosTramitesArchivosPrecarga>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Regtra.IDREGISTROTRAMITEDOCUMENTO.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.Modificar_PreCarga(idRegistroTramiteDocumento, item, Regtra);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite modificado con éxito",
                    IdRegistroTramite = Regtra.IDREGISTROTRAMITEDOCUMENTO

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }


    }
}
