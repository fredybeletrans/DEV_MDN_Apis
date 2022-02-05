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
using Microsoft.IdentityModel.Tokens;

namespace CoreApiWeaponRegister.Controllers
{
    //[Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext ctx;

        private IUsuarioRepository _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
                
        public UsuariosController(IUsuarioRepository userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            //ctx = _context;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        //public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            //var user = _mapper.Map<Usuarios_pw>(model);

            var user = _userService.Authenticate(model.USUARIO, model.PASSWORDHASH);// quitar sino funciona

            if (user == null)
                return BadRequest(new { message = "Usuario o Clave son incorrectas" });

            if (user.ESTADO == "REGISTRADO")
                return BadRequest(new { message = "Usuario sin confirmar" });

            //try 
            //{
            //    // create user
            //    var user = _userService.Authenticate(model); 

            return Ok(new
                {
                    NombreUsuario = user.USUARIO,
                    Nombres = user.NOMBRES,
                    Apellidos = user.APELLIDOS,
                    Estado = user.ESTADO
                    //Token = user.TOKENVERIFICACION
                });
            //}
            //catch (AppException ex)
            //{
            //    // return error message if there was an exception
            //    return BadRequest(new { message = ex.Message });
            //}

            //if (user == null)
            //{
            //    return BadRequest(new { message = "Username or password is incorrect" });
            //}

            //var tokenHandler = new JwtSecurityTokenHandler();
            ///var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Id.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            //return Ok(new
            //{
            //    Id = user.Id,
            //    Username = user.Username,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Token = tokenString
            //});
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<Usuarios_pw>(model);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.USUARIO.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),// .AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            try
            {
                // create user
                _userService.Create(user, model.PASSWORDHASH, tokenString);
                //return Ok();

                return Ok(new
                {
                    Success = "Ok",
                    ReasonCode = "00",
                    message = "Usuario creado con éxito",
                    NombreUsuario = user.USUARIO,
                    Correo = user.CORREO,
                    Nombres = user.NOMBRES,
                    Apellidos = user.APELLIDOS,
                    Estado = user.ESTADO,
                    Token = user.TOKENVERIFICACION
                    
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("VerifEmailUsuario/{tokenverif}")]
        //public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        public IActionResult VerifEmailUsuario(string tokenverif)
        {
            

            if (tokenverif == null || tokenverif=="")
                return BadRequest(new { message = "Token vacio no se puede validar" });
            
          var val=  _userService.VerifEmailUsuario(tokenverif);
            if(val==false)
            return BadRequest(new { message = "Es posible que la cuenta haya sido validada" });

            return Ok(new
            {
                Valido = val,
               
            });
            
        }

    }
}
