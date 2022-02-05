using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreApiWeaponRegister.Entities.Models.Usuarios
{
    public class AuthenticateModel
    {
        [Required]
        public string USUARIO { get; set; }

        [Required]
        public String PASSWORDHASH { get; set; }
    }
}
