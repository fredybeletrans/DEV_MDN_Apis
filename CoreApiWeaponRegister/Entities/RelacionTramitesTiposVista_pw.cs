using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("RELACIONTRAMITESTIPOSVISTA_PW")]
    public class RelacionTramitesTiposVista_pw
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id tramite es requerido")]
        public int IDTRAMITE { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Id tipo tramite es requerido")]
        public int IDTIPOTRAMITE { get; set; }
        public string NOMBRE { get; set; }
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }
    }
}
