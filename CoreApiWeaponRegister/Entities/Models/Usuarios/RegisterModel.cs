using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities.Models.Usuarios
{
    public class RegisterModel
    {
        //public int IDUSUARIO { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string NOMBRES { get; set; }

        [Required]
        public string APELLIDOS { get; set; }

        [Required]
        public String PASSWORDHASH { get; set; }

        //public string FECHAMODIFICACION { get; set; } //Enviar NULL desde el Controller y se mapea desde clase UsuarioRepository

        public string USUARIOMOD { get; set; } //Enviar NULL desde el Controller y se mapea desde clase UsuarioRepository

        public String PASSWORDSALT { get; set; } //Enviar NULL desde el Controller, se mapea desde clase UsuarioRepository

        //public string FECHAVERIFICACIONCUENTA { get; set; } //Enviar NULL desde el Controller, se actualiza cuando se verifica cuenta

        //[Required]
        public string TOKENVERIFICACION { get; set; } //Se envia desde Controller la generacion de Token

        [Required]
        public string CORREO { get; set; }

        public string ESTADO { get; set; } //Enviar el Estado C - Creado desde el Controller
    }
}
