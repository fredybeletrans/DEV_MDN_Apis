using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CoreApiWeaponRegister.Entities.Models.RegistrosTramites
{
    public class RegisterTramitesModel
    { 
        public int IDREGISTROTRAMITE { get; set; }
        public int IDPERSONA { get; set; }
        public int IDTRAMITE { get; set; }
        public int IDTRAMITEOPCION { get; set; }
        public int IDTIPOTRAMITE { get; set; }
        public int NUMTRAMITE { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }
        public int? NUMSOLICITUD { get; set; }
        public DateTime FECHASOLICITUD { get; set; }
        public string? ESTADO { get; set; }
    }
}
