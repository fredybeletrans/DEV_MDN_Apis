using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("REGISTROSTRAMITES_PW")]
    public partial class RegistrosTramites_pw
    {
        [Key]
        [Required(ErrorMessage = "Id registro es requerido")]
        public int IDREGISTROTRAMITE { get; set; }
        public int IDPERSONA { get; set; }
        public int IDTRAMITE { get; set; }
        public int IDTRAMITEOPCION { get; set; }
        public int IDTIPOTRAMITE { get; set; }
        public int NUMTRAMITE { get; set; }
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }
        public int? NUMSOLICITUD { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FECHASOLICITUD { get; set; }
        public string? ESTADO { get; set; }

    }
}
