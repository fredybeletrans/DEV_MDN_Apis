using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("TIPOSDOCUMENTOS_PW")]
    public class TiposDocumentos_pw
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[JsonIgnore]
        [Required(ErrorMessage = "Id Genero es requerido")]
        public int IDTIPODOCUMENTO { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [Required]
        public string ESTADO { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }
    }
}
