using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreApiWeaponRegister.Entities.Models.Usuarios
{
    public class RegisterRequest
    {
        [Required]
        public string USUARIO { get; set; }
                
        [Required]
        public string NOMBRES { get; set; }

        [Required]
        public string APELLIDOS { get; set; }

        [Required]
        public string PASSWORDHASH { get; set; }
                
        public DateTime FECHAMODIFICACION { get; set; }
        
        public string IDUSUARIOMOD { get; set; }
                
        public string PASSWORDSALT { get; set; }

        public DateTime? FECHAVERIFICACIONCUENTA { get; set; }
                
        public string TOKENVERIFICACION { get; set; }

        [Required]
        public string CORREO { get; set; }
        
        public string ESTADO { get; set; }
    }
}
