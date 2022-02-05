using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CoreApiWeaponRegister.Entities.Models.RegistrosTramites
{
    public class RegisterTramitesEstadosModel
    {
        public int IDREGISTROTRAMITE { get; set; }
        public string ESTADO { get; set; }
        public string MOTIVO { get; set; }
        public string USUARIO { get; set; }
        public DateTime FECHA { get; set; }
       
    }
}
