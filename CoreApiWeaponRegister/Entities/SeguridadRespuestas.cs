using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    public class SeguridadRespuestas
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id persona es requerido")]
        public int IDPERSONA { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Id pregunta es requerido")]
        public int IDPREGUNTA { get; set; }
        public string RESPUESTA { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }
    }
}
