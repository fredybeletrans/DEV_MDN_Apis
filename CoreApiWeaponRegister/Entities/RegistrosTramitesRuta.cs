using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("REGISTROSTRAMITESRUTA")]
    public partial class RegistrosTramitesRuta
    {
        [Key]
        [Required(ErrorMessage = "Id registro es requerido")]
        public int IDREGISTROTRAMITE { get; set; }
        public int IDPERSONA { get; set; }
        public string NombreTramite { get; set; }
        public string NombreTipoTramite { get; set; }

        

    }
}
