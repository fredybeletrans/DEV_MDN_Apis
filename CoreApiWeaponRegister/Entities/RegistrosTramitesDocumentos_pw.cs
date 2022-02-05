using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("REGISTROSTRAMITESDOCUMENTOS_PW")]
    public partial class RegistrosTramitesDocumentos_pw
    {
        [Key]
        [Required(ErrorMessage = "Id registro es requerido")]
        public int IDREGISTROTRAMITEDOCUMENTO { get; set; }
        public int IDDOCUMENTO { get; set; }
        public int IDREGISTROTRAMITE { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FECHAVIGENCIA { get; set; }
        public string ESTADO { get; set; }
        public int? CADUCADO { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FECHAEXPIRACION { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime?  FECHAEMISION { get; set; }

    }
}
