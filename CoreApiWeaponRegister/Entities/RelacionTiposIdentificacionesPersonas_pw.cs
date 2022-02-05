using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("RELACIONTIPOSIDENTIFICACIONESPERSONAS_PW")]
    public partial class RelacionTiposIdentificacionesPersonas_pw
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id tipo persona es requerido")]
        public int IDTIPOPERSONA { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Id Tipo de identificación es requerido")]
        public int IDTIPOIDENTIFICACIONPERSONAL { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }

    }
}
