using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("RELACIONTIPOSIDENTIFICACIONESPERSONASVISTA_PW")]
    public partial class RelacionTiposIdentificacionesPersonasVista_pw
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id tipo persona es requerido")]
        public int IDTIPOPERSONA { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Id Tipo de identificación es requerido")]
        public int IDTIPOIDENTIFICACIONPERSONAL { get; set; }

        public string NOMBRE { get; set; }
        public string MASCARA { get; set; }

    }
}
