using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("RELACIONTRAMITESTIPOS_PW")]
    public partial class RelacionTramitesTipos_pw
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id TRAMITE es requerido")]
        public int IDTRAMITE { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Id tramite opciones es requerido")]
        public int IDTIPOTRAMITE { get; set; }
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }

    }
}
