using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("REGISTROSTRAMITESAPROBACION")]
    public class RegistrosTramitesAprobacion
    {
        [Key]
        [Required(ErrorMessage = "Id registro es requerido")]
        public int IDREGISTROTRAMITE { get; set; }
        public int IDPERSONA { get; set; }
        public string NOMBREPERSONA { get; set; }
        public int IDTRAMITE { get; set; }
        public string NOMBRETRAMITE { get; set; }
        public int IDTRAMITEOPCION { get; set; }
        public string NOMBRETRAMITEOPCION { get; set; }
        public int IDTIPOTRAMITE { get; set; }
        public string NOMBRETIPOTRAMITE { get; set; }
        public DateTime FECHASOLICITUD { get; set; }
        public string ESTADO { get; set; }
    }
}
