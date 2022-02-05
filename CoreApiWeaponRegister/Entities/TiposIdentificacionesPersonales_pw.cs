using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("TIPOSIDENTIFICACIONESPERSONALES_PW")]
    public partial class TiposIdentificacionesPersonales_pw
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id Tipo de identificación es requerido")]

        public int IDTIPOIDENTIFICACIONPERSONAL { get; set; }
        [Required]
        public string NOMBRE { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }

        
        public string MASCARA { get; set; }



    }
}
