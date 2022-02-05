using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("TRAMITESDOCUMENTOS_PW")]
    public partial class TramitesDocumentos_pw
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id tramite opcion es requerido")]
        public int IDTRAMITEOPCION { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Id  tipo tramite es requerido")]
        public int IDTIPOTRAMITE { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Id documentos es requerido")]
        public int IDDOCUMENTO { get; set; }
        public string ESTADO { get; set; }
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }


    }
}
