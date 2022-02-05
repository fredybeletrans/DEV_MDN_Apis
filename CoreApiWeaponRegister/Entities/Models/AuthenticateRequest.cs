using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string PASSWORDHASH { get; set; }

        //[JsonIgnore]        
        //public string ESTADO { get; set; }
    }
}
