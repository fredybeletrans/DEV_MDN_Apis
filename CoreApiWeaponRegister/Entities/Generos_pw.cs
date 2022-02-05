using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("GENEROS_PW")]
    public partial class Generos_pw
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[JsonIgnore]
        [Required(ErrorMessage = "Id Genero es requerido")]
        public int IDGENERO { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }
    }
}
