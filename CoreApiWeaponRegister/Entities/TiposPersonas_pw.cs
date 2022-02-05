using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("TIPOSPERSONAS_PW")]
    public partial class TiposPersonas_pw
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[JsonIgnore]
        [Required(ErrorMessage = "Id Tipo Persona es requerido")]
        public int IDTIPOPERSONA { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }
    }
}
