using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities.Models
{
    public class AuthenticateResponse
    {
        public int USUARIO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string ESTADO { get; set; }
    }
}
