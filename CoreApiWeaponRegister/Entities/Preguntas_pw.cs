using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("PREGUNTAS_PW")]
    public class Preguntas_pw
    {
        [Key]
        [Required(ErrorMessage = "Id tipo persona es requerido")]
        public int IDPREGUNTA { get; set; }
        public string DESCRIPCION { get; set; }
        public int IDGRUPOPREGUNTA { get; set; }
        public int IDTIPORESPUESTA { get; set; }
        public string TIPODATO { get; set; }

    }
}
