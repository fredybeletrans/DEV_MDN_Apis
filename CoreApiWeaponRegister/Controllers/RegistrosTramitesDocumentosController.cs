
using Microsoft.IdentityModel.Tokens;
using CoreApiWeaponRegister.Entities.Models.RegistrosTramitesDocumentos;
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
    public class RegistrosTramitesDocumentosController : ControllerBase
    {
        private readonly DataContext ctx;

        private IRegistrosTramitesDocumentosRepository _perService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RegistrosTramitesDocumentosController(IRegistrosTramitesDocumentosRepository perService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _perService = perService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }



        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterTramitesDocumentosModel model)
        {
            // map model to entity
            var Regtra = _mapper.Map<RegistrosTramitesDocumentos_pw>(model);

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
                    IdRegistroTramite = Regtra.IDREGISTROTRAMITE

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPut("Modify/{idregistrotramitedocumento}")]
        public IActionResult Modify(int idregistrotramitedocumento, [FromBody] RegisterTramitesDocumentosModel model)
        {
            // map model to entity
            var Regtra = _mapper.Map<RegistrosTramitesDocumentos_pw>(model);

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
                _perService.Modificar(idregistrotramitedocumento, Regtra);
                ////return Ok();

                //return Ok(new 
                //{
                //    Success = "Ok",
                //    ReasonCode = "00",
                //    message = "RegistrosTramite modificado con éxito",
                //    IdRegistroTramite = Regtra.IDREGISTROTRAMITE

                //});
                var resul = _perService.ObtenerRegistrosTramiteDocumentosById(idregistrotramitedocumento);
                return Ok(resul);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

   

        [AllowAnonymous]
        [HttpPost("register_batch")]
        public IActionResult register_batch([FromBody] RegisterBatchModel model)
        {
            //var Regtra = _mapper.Map<RegistrosTramitesDocumentos_pw>(model);

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
                _perService.Create_Batch(model.IDREGISTROTRAMITE,model.IDTRAMITEOPCION,model.IDTIPOTRAMITE);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "RegistrosTramite creado con éxito",
                    IdRegistroTramite = model.IDREGISTROTRAMITE

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteDocumentos/{idregistrotramite}")]
        public IActionResult ObtenerRegistrosTramiteDocumentos(int idregistrotramite)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramiteDocumentos(idregistrotramite);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesDocumentos_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteDocumentosConsulta/{idregistrotramite}")]
        public IActionResult ObtenerRegistrosTramiteDocumentosConsulta(int idregistrotramite)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramiteDocumentosConsulta(idregistrotramite);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesDocumentosVista_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteDocumentosById/{idregistrotramitedocumento}")]
        public IActionResult ObtenerRegistrosTramiteDocumentosById(int idregistrotramitedocumento)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramiteDocumentosById(idregistrotramitedocumento);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesDocumentosVista_pw>>(relacion);
                return Ok(estadosResult);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [AllowAnonymous]
        [HttpGet("ObtenerRegistrosTramiteDocumentosAprobaciones/{idregistrotramitedocumento}")]
        public IActionResult ObtenerRegistrosTramiteDocumentosAprobaciones(int idregistrotramitedocumento)
        {
            try
            {
                var relacion = _perService.ObtenerRegistrosTramiteDocumentosAprobaciones(idregistrotramitedocumento);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<RegistrosTramitesDocumentosAprobaciones>>(relacion);
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
