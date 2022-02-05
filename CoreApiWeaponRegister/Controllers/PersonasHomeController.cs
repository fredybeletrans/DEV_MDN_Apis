//using CoreApiWeaponRegister.Helpers;
//using CoreApiWeaponRegister.Data;
//using CoreApiWeaponRegister.Entities;
//using CoreApiWeaponRegister.Entities.Models;
//using CoreApiWeaponRegister.Repository.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authorization;
//using AutoMapper;
//using Microsoft.Extensions.Options;
//using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
using Microsoft.IdentityModel.Tokens;
using CoreApiWeaponRegister.Entities.Models.Personas;

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
    public class PersonasController : ControllerBase
    {
        private readonly DataContext ctx;

        private IPersonasRepository _perService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public PersonasController(IPersonasRepository perService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _perService = perService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterPersonasModel model)
        {
            // map model to entity
            var per = _mapper.Map<Personas_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, per.IDPERSONA.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create per
                _perService.Create(per);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "Persona creado con éxito"

                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        [AllowAnonymous]
        [HttpGet("ObtenerPersona/{user}")]
        public IActionResult ObtenerPersona(string user)
        {
            try
            {
                var relacion = _perService.ObtenerPersona( user);
                //_logger.LogInfo($"Returned all owners from database.");

                var estadosResult = _mapper.Map<IEnumerable<Personas_pw>>(relacion);
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
